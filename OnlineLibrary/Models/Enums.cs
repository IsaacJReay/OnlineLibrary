using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models;

public class Enums
{
    public enum Roles {
        Teacher,
        Student
    }
    public enum Genders
    {
        Male,
        Female
    }

    public enum Faculties {
        [Display(Name="Faculty of Economic and Agriculture")]
        Economic_Agriculture,
        [Display(Name="Faculty of Tourism and Hospitality")]
        Tourism_Hospitality,
        [Display(Name="Faculty of Engineering and Architecture")]
        Engineering_Architecture,
        [Display(Name="Faculty of Law and Social Sciences")]
        Law_SocialSciences,
        [Display(Name="Faculty of Business Administration")]
        Business_Administration,
        [Display(Name="Faculty of Science and Technology")]
        Science_Technology,
        [Display(Name="Faculty of Art, Humanities, and Languages")]
        Art_Humanities_Languages,
        [Display(Name="School of Doctoral Studies")]
        Doctoral_Studies
    }

    public enum FileType {
        PDF,
        DOCX,
        XLSX,
        PPTX,
        ZIP,
        EXE
    }
}