using Shared.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSiteLibrary.Model.DocumentCompilation
{
    class DocumentCompilationUtils
    {

        #region Medoti di visualizzazione per Firme

        public static string PrintCompanyFromSignature(SignatureModel signature, DocumentModel document)
        {
            var compName = "";
            var company = document.Companies.Where(x => x.Id == signature.CompanyId).FirstOrDefault();
            if (company is not null)
            {
                compName = string.IsNullOrEmpty(company.CompanyName) ? company.SelfEmployedName : company.CompanyName;
            }
            return compName;
        }

        public static string PrintQuestionForReported(int idQuestion, DocumentModel document)
        {
            var questionNumber = "";
            foreach (var category in document.Categories)
            {
                var q = category.Questions.Where(x => x.Id == idQuestion).SingleOrDefault();
                if (q is not null)
                {
                    questionNumber = $"{category.Order}.{q.Order} ";
                    break;
                }
            }
            return questionNumber;
        }

        #endregion

        #region Metodi di visualizzazione per Question

        public static string ReportedCompany(List<int> reportedCompanyIds, DocumentModel document)
        {
            var message = reportedCompanyIds.Count > 0 ? "(" : "";
            foreach (var companyId in reportedCompanyIds)
            {
                var companyName = document.Companies.Where(x => x.Id == companyId).Select(x => x.CompanyName).FirstOrDefault() ?? "";
                message += $"{companyName}, ";
            }

            if (message.EndsWith(", "))
            {
                //rimuovo l'ultima virgola
                message = message.Remove(message.Length - 2);
                message += ")";
            }

            return message;
        }

        public static string PrintDocumentField(string? field)
        {
            return string.IsNullOrEmpty(field) ? "" : field;
        }

        public static string CategoryNumber(DocumentCategoryModel cat)
        {
            return cat.Order + ".";
        }

        public static string CategoryText(DocumentCategoryModel cat)
        {
            return cat.Text;
        }

        public static string QuestionText(DocumentCategoryModel cat, string questionText, int order)
        {
            return cat.Order + "." + order + " " + questionText;
        }

        #endregion
    }
}
