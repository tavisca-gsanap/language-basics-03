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
            int[] dietsWithMinimumProtein = frequency.FrequencyCalculator( protein,protein.Min());
            int[] dietsWithMaximumProtein = frequency.FrequencyCalculator(protein, protein.Max());
            int[] dietsWithMinimumCarbs = frequency.FrequencyCalculator(carbs, carbs.Min());
            int[] dietsWithMaximumCarbs = frequency.FrequencyCalculator(carbs, carbs.Max());
            int[] dietsWithMinimumFat = frequency.FrequencyCalculator(fat, fat.Min());
            int[] dietsWithMaximumFat = frequency.FrequencyCalculator(fat, fat.Max());
            //now each of above array will contain indices of diets eligible. will contain more than one in case of ties.
            //following will calculate protein ammount in each diet.
            for (int i = 0; i < protein.Length; i++)
            {
                calories[i] = 9 * fat[i] + 5 * (protein[i] + carbs[i]);
            }
            int[] dietsWithMinimumCalories = frequency.FrequencyCalculator(calories, calories.Min());
            int[] dietsWithMaximumCalories = frequency.FrequencyCalculator(calories, calories.Max());
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

                    if (diet[j] == 'p')
                    {
                        if (dietsWithMinimumProtein.Length > 1)
                        {
                            foreach (int k in dietsWithMinimumProtein) dietsTillNow.Add(k);
                            flag = true;
                            j++;
                        }
                        else
                        {
                            if (flag && !dietsTillNow.Contains(dietsWithMinimumProtein[0]))
                            {
                                outputDiets[i] = dietsTillNow[0];
                                j++;
                            }
                            else
                            {
                                outputDiets[i] = dietsWithMinimumProtein[0];
                                break;
                            }
                        }
                    }
                    else if (diet[j] == 'P')
                    {
                        if (dietsWithMaximumProtein.Length > 1)
                        {
                            j++;
                            foreach (int k in dietsWithMaximumProtein) dietsTillNow.Add(k);
                            flag = true;
                        }
                        else
                        {
                            if (flag && !dietsTillNow.Contains(dietsWithMaximumProtein[0]))
                            {
                                outputDiets[i] = dietsTillNow[0];
                                j++;
                            }
                            else
                            {
                                outputDiets[i] = dietsWithMaximumProtein[0];
                                break;
                            }
                        }
                    }
                    else if (diet[j] == 'c')
                    {
                        if (dietsWithMinimumCarbs.Length > 1)
                        {
                            j++;
                            foreach (int k in dietsWithMinimumCarbs) dietsTillNow.Add(k);
                            flag = true;
                        }
                        else
                        {
                            if (flag && !dietsTillNow.Contains(dietsWithMinimumCarbs[0]))
                            {
                                outputDiets[i] = dietsTillNow[0];
                                j++;
                            }
                            else
                            {
                                outputDiets[i] = dietsWithMinimumCarbs[0];
                                break;
                            }
                        }
                    }
                    else if (diet[j] == 'C')
                    {
                        if (dietsWithMaximumCarbs.Length > 1)
                        {
                            j++;
                            foreach (int k in dietsWithMaximumCarbs) dietsTillNow.Add(k);
                            flag = true;
                        }
                        else
                        {
                            if (flag && !dietsTillNow.Contains(dietsWithMaximumCarbs[0]))
                            {
                                outputDiets[i] = dietsTillNow[0];
                                j++;
                            }
                            else
                            {
                                outputDiets[i] = dietsWithMaximumCarbs[0];
                                break;
                            }
                        }
                    }
                    else if (diet[j] == 'f')
                    {
                        if (dietsWithMinimumFat.Length > 1)
                        {
                            j++;
                            foreach (int k in dietsWithMinimumFat) dietsTillNow.Add(k);
                            flag = true;
                        }
                        else
                        {
                            if (flag && !dietsTillNow.Contains(dietsWithMinimumFat[0]))
                            {
                                outputDiets[i] = dietsTillNow[0];
                                j++;
                            }
                            else
                            {
                                outputDiets[i] = dietsWithMinimumFat[0];
                                break;
                            }
                        }
                    }
                    else if (diet[j] == 'F')
                    {
                        if (dietsWithMaximumFat.Length > 1)
                        {
                            j++;
                            foreach (int k in dietsWithMaximumFat) dietsTillNow.Add(k);
                            flag = true;
                        }
                        else
                        {
                            if (flag && !dietsTillNow.Contains(dietsWithMaximumFat[0]))
                            {
                                outputDiets[i] = dietsTillNow[0];
                                j++;
                            }
                            else
                            {
                                outputDiets[i] = dietsWithMaximumFat[0];
                                break;
                            }
                        }
                    }
                    else if (diet[j] == 't')
                    {
                        if (dietsWithMinimumCalories.Length > 1)
                        {
                            j++;
                            foreach (int k in dietsWithMinimumCalories) dietsTillNow.Add(k);
                            flag = true;
                        }
                        else
                        {
                            if (flag && !dietsTillNow.Contains(dietsWithMinimumCalories[0]))
                            {
                                outputDiets[i] = dietsTillNow[0];
                                j++;
                            }
                            else
                            {
                                outputDiets[i] = dietsWithMinimumCalories[0];
                                break;
                            }
                        }
                    }
                    else if (diet[j] == 'T')
                    {
                        if (dietsWithMaximumCalories.Length > 1)
                        {
                            j++;
                            foreach (int k in dietsWithMaximumCalories) dietsTillNow.Add(k);
                            flag = true;
                        }
                        else
                        {
                            if (flag && !dietsTillNow.Contains(dietsWithMaximumCalories[0]))
                            {
                                outputDiets[i] = dietsTillNow[0];
                                j++;
                            }
                            else
                            {
                                outputDiets[i] = dietsWithMaximumCalories[0];
                                break;
                            }
                        }
                    }
                    else
                    {
                        outputDiets[i] = 0;
                    }
                }
            }
            return outputDiets;
        }
    }
}
