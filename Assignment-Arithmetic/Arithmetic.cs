// Assignment Arithmetic Template. S Manoharan.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilities.Courses
{
   public class Arithmetic : TemplatedCoursework
   {
      public override void Initialize()
      {
         base.Initialize();
      } // Initialize

      public override string Answers(bool withAnswers, uint uid)
      {
         InitInputs(uid);
         StringBuilder sb = new StringBuilder();
         sb.AppendFormat("AUID: {0}", uid);
         sb.AppendLine();
         WriteAnswers(sb, GetSum(uid), "1", withAnswers);
         WriteAnswers(sb, GetProduct(uid), "2", withAnswers);

         return sb.ToString();
      } // Answers

      private void WriteAnswers(StringBuilder sb, string answer, string partId, bool withAnswers)
      {
         sb.AppendFormat("{0}: {1}", partId, withAnswers ? answer : "");
         sb.AppendLine();
      } // WriteAnswers

      public override string MarkingResult(bool withAnswers, uint uid, string submission, out double mark)
      {
         InitInputs(uid);
         mark = 0.0;
         StringBuilder sb = new StringBuilder();

         Dictionary<string, string> kvp = ProcessAnswerFile(submission);
         string a1_aS = SubmittedAnswer(kvp, "1");
         string a2_aS = SubmittedAnswer(kvp, "2");

         if (withAnswers)
         {
            sb.AppendFormat("1: correct answer: [{0}]; your answer: [{1}]\n",
               GetSum(uid), a1_aS);
            sb.AppendFormat("2: correct answer: [{0}]; your answer: [{1}]\n",
               GetProduct(uid), a2_aS);
         }

         if (a1_aS == GetSum(uid)) mark += 1;
         if (a2_aS == GetProduct(uid)) mark += 1;

         sb.AppendLine();
         sb.AppendFormat("Your total marks: {0}/2\n", mark);
         return sb.ToString();
      } // MarkingResult

      private static string SubmittedAnswer(Dictionary<string, string> kvp, string qId)
      {
         string a = kvp.ContainsKey(qId) ? Regex.Replace(kvp[qId], @"\s+", "") : "";
         return a;
      } // SubmittedAnswer

      private void InitInputs(uint uid)
      {
         Random random = GetRandom(uid);
         int maxNums = random.Next(5, 9);
         numbers.Clear();
         for (int i = 0; i < maxNums; ++i)
         {
            numbers.Add(random.Next(1, 9));
         }
      } // InitInputs

      public string GetNumbers(uint uid)
      {
         InitInputs(uid);
         StringBuilder sb = new StringBuilder();
         sb.AppendFormat("{0}", numbers[0]);
         for (int i = 1; i < numbers.Count; ++i)
         {
            sb.AppendFormat(", {0}", numbers[i]);
         }
         return sb.ToString();
      } // GetNumbers

      public string GetSum(uint uid)
      {
         InitInputs(uid);
         int sum = 0;
         for (int i = 0; i < numbers.Count; ++i)
         {
            sum += numbers[i];
         }
         return sum.ToString();
      } // GetSum

      public string GetProduct(uint uid)
      {
         InitInputs(uid);
         int prod = 1;
         for (int i = 0; i < numbers.Count; ++i)
         {
            prod *= numbers[i];
         }
         return prod.ToString();
      } // GetProduct

      List<int> numbers = new List<int>();
   } // class
} // namespace
