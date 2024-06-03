using System.Text.Json;
using ConstructionSiteLibrary.Managers;
using Shared;
using Shared.Defaults;
using Shared.Templates;


namespace ConstructionSiteLibrary.Repositories;

public class CategoriesRepository(HttpManager httpManager)
{
    List<TemplateCategoryModel> Categories = [];

    List<SubjectModel> Subjects = [];

    private HttpManager _httpManager = httpManager;

    #region  Categories

    public async Task<List<TemplateCategoryModel>> GetCategories()
    {
        if (Categories.Count == 0)
        {
            try
            {
                var response = await _httpManager.SendHttpRequest("Category/CategoriesList", "");
                if (response.Code.Equals("0"))
                {
                    Categories = JsonSerializer.Deserialize<List<TemplateCategoryModel>>(response.Content.ToString() ?? "") ?? [];
                }
            }
            catch (Exception e) 
            { 
                var msg = e.Message;
            }
        }

        return Categories;
    }

    public async Task<bool> SaveCategory(TemplateCategoryModel category)
    {
        var response = await _httpManager.SendHttpRequest("Category/SaveCategory", category);

        //NotificationService.Notify(response);
        if (response.Code.Equals("0"))
        {
            Categories.Clear();
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateCategories(List<TemplateCategoryModel> categories)
    {
        var response = await _httpManager.SendHttpRequest("Category/UpdateCategories", categories);
        //NotificationService.Notify(response);
        if (response.Code.Equals("0"))
        {
            Categories.Clear();
            return true;
        }

        return false;
    }

    
    public async Task<bool> HideCategories(List<TemplateCategoryModel> categories)
    {
        var response = await _httpManager.SendHttpRequest("Category/HideCategories", categories);
        //NotificationService.Notify(response);
        if (response.Code.Equals("0"))
        {
            Categories.Clear();
            return true;
        }

        return false;
    }

    #endregion

    #region Subjects

    public async Task<List<SubjectModel>> GetSubjects()
    {
        if (Subjects.Count == 0)
        {
            var response = await _httpManager.SendHttpRequest("Category/SubjectsList", "");
            if (response.Code.Equals("0"))
            {
                Subjects = JsonSerializer.Deserialize<List<SubjectModel>>(response.Content.ToString() ?? "") ?? [];
            }
        }

        return Subjects;
    }

    public async Task<bool> SaveSubject(SubjectModel subject)
    {
        var response = await _httpManager.SendHttpRequest("Category/SaveSubject", subject);

        //NotificationService.Notify(response);
        if (response.Code.Equals("0"))
        {
            Subjects.Clear();
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateSubjects(List<SubjectModel> subjects)
    {
        var response = await _httpManager.SendHttpRequest("Category/UpdateSubjects", subjects);
        //NotificationService.Notify(response);
        if (response.Code.Equals("0"))
        {
            Subjects.Clear();
            return true;
        }

        return false;
    }

    
    public async Task<bool> HideSubjects(List<SubjectModel> subjects)
    {
        var response = await _httpManager.SendHttpRequest("Category/HideSubjects", subjects);
        //NotificationService.Notify(response);
        if (response.Code.Equals("0"))
        {
            Subjects.Clear();
            return true;
        }

        return false;
    }

    #endregion 

}
