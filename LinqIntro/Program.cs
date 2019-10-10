using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = new List<int>()
            {
                1,2,3,4,7,93,355,321,221,8
            };

            // Lambda syntax
            // Find (or .find in JavaScript)
            int foundNum = nums.Find(n => n == 93);

            // Where (or .filter in JavaScript). Using ToList gets rid of the IEnumerable error
            List<int> filteredNums = nums.Where(n => n < 100).ToList();

            // OrderBy (or .sort in JS)
            List<int> orderedAndFiltered = nums
                .Where(n => n < 100)
                .OrderBy(n => n)
                .ToList();

            // One usecase for JS .reduce method
            int sum = nums.Sum();

            int smallest = nums.Min();

            int biggest = nums.Max();

            List<int> doubled = nums.Select(num => num * 2).ToList();

            // Select (.map in JS)
            List<string> stringifiedNums = nums.Select(n =>
            {
                return n.ToString();
            }).ToList();

            // Query Linq Syntax
            List<int> cohortStudentCount = new List<int>()
                {
                    25, 12, 28, 22, 11, 25, 27, 24, 19
                };
            Console.WriteLine($"Largest cohort was {cohortStudentCount.Max()}");
            Console.WriteLine($"Smallest cohort was {cohortStudentCount.Min()}");
            Console.WriteLine($"Total students is {cohortStudentCount.Sum()}");

            // Query syntax has to end with a "select"
            IEnumerable<int> idealSizes = from count in cohortStudentCount
                                          where count < 27 && count > 19
                                          orderby count ascending
                                          select count;

            // Same as above using lambda syntax
            IEnumerable<int> idealSizesLambda = cohortStudentCount
                .Where(count => count < 27 && count > 19)
                .OrderBy(count => count);

            Console.WriteLine($"Average ideal size is {idealSizes.Average()}");

            // The @ symbol lets you create multi-line strings in C#
            Console.WriteLine($@"
                There were {idealSizes.Count()} ideally sized cohorts
                There have been {cohortStudentCount.Count()} total cohorts");


            /*
            ----------------------------
            LINQ with complex objects
            ----------------------------
             */
            List<Shape> shapes = new List<Shape>()
            {
              new Shape() {NumberOfSides = 3, Height = 10.5, Width = 15.2, Color = "Orange"},
              new Shape() {NumberOfSides = 4, Height = 10.5, Width = 10.5, Color = "Red"},
              new Shape() {NumberOfSides = 1, Height = 10, Width = 10, Color = "Blue"},
            };

            Shape foundSquare = shapes.Find(shape => shape.NumberOfSides == 4);

            List<Shape> notCircles = shapes.Where(shape => shape.NumberOfSides > 1).ToList();

            double sumOfHeights = shapes.Select(shape => shape.Height).Sum();

            List<Shape> andyShapes = new List<Shape>()
            {
              new Shape() {NumberOfSides = 3, Height = 10.5, Width = 15.2, Color = "Orange", IsFiveYearOldApproved = true},
              new Shape() {NumberOfSides = 4, Height = 10.5, Width = 10.5, Color = "Red", IsFiveYearOldApproved = true},
              new Shape() {NumberOfSides = 1, Height = 10, Width = 10, Color = "Orange", IsFiveYearOldApproved = true},
              new Shape() {NumberOfSides = 1, Height = 10, Width = 10, Color = "Red", IsFiveYearOldApproved = true},
              new Shape() {NumberOfSides = 1, Height = 10, Width = 10, Color = "Blue", IsFiveYearOldApproved = false},
            };

            var shapeGroups = andyShapes.GroupBy(shape => shape.Color);
            var kidsLoveShapes = andyShapes.GroupBy(shape => shape.IsFiveYearOldApproved);

            var counts = andyShapes
                .GroupBy(shape => shape.Color)
                .Select(group => new ShapeReport()
                {
                    ColorName = group.Key,
                    ShapeCount = group.Count()
                })
                .ToList();
        }
        
        public class ShapeReport
        {
            public string ColorName { get; set; }
            public int ShapeCount { get; set; }
        }

    }
}
