using System;
using System.Collections.Generic;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise3
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        //following function will return array of indices(indicating diet plan) with value same as given value. 
        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            Frequency frequency = new Frequency();
            int[] calories = new int[protein.Length];
            int[] outputDiets = new int[dietPlans.Length];
            Dictionary<string, int[]> dietsWithSpecifiedValues = new Dictionary<string, int[]>();
            dietsWithSpecifiedValues.Add("p", frequency.FrequencyCalculator(protein, protein.Min()));
            dietsWithSpecifiedValues.Add("P", frequency.FrequencyCalculator(protein, protein.Max()));
            dietsWithSpecifiedValues.Add("c", frequency.FrequencyCalculator(carbs, carbs.Min()));
            dietsWithSpecifiedValues.Add("C", frequency.FrequencyCalculator(carbs, carbs.Max()));
            dietsWithSpecifiedValues.Add("f", frequency.FrequencyCalculator(fat, fat.Min()));
            dietsWithSpecifiedValues.Add("F", frequency.FrequencyCalculator(fat, fat.Max()));
            //now each of above array will contain indices of diets eligible. will contain more than one in case of ties.
            //following will calculate calorie ammount in each diet.
            for (int i = 0; i < protein.Length; i++)
            {
                calories[i] = 9 * fat[i] + 5 * (protein[i] + carbs[i]);
            }
            dietsWithSpecifiedValues.Add("t", frequency.FrequencyCalculator(calories, calories.Min()));
            dietsWithSpecifiedValues.Add("T", frequency.FrequencyCalculator(calories, calories.Max()));
            /*Now the logic is simple we will first check first character of given string and will check whether length of it's 
            array is grater than 1 or not if not then the first element is our answer if no ww will go for next char if next char
            has index which was present in earlier array then that index is our answer if not then go for next char or first element from
            previous array is our answer.*/
            for (int i = 0; i < dietPlans.Length; i++)
            {
                char[] diet = dietPlans[i].ToCharArray();
                int j = 0;
                IList<int> dietsTillNow = new List<int>();
                bool flag = false;
                while (j < diet.Length)
                {
                    try
                    {
                        if (dietsWithSpecifiedValues[diet[j]+""].Length > 1)
                        {
                            foreach (int k in dietsWithSpecifiedValues[diet[j] + ""]) dietsTillNow.Add(k);
                            flag = true;
                            j++;
                        }
                        else
                        {
                            if (flag && !dietsTillNow.Contains(dietsWithSpecifiedValues[diet[j] + ""][0]))
                            {
                                outputDiets[i] = dietsTillNow[0];
                                j++;
                            }
                            else
                            {
                                outputDiets[i] = dietsWithSpecifiedValues[diet[j] + ""][0];
                                break;
                            }
                        }
                    }
                    catch
                    {
                        outputDiets[i] = 0;
                    }
                }
            }
            return outputDiets;
        }
    }
}
