namespace OnlineLibrary.Models;

public class Enums
{
    public enum Roles
    {
        Teacher,
        Student,
        Admin
    }

    public enum Genders
    {
        Male,
        Female
    }

    public enum Faculties
    {
        Faculty_of_Economic_and_Agriculture,
        Faculty_of_Tourism_and_Hospitality,
        Faculty_of_Engineering_and_Architecture,
        Faculty_of_Law_and_Social_Sciences,
        Faculty_of_Business_Administration,
        Faculty_of_Science_and_Technology,
        Faculty_of_Art_Humanities_and_Languages,
        Faculty_of_Doctoral_Studies
    }

    public enum FileType
    {
        pdf,
        docx,
        odt,
        xlsx,
        ods,
        pptx,
        odp,
        zip,
        rar,
        exe,
        msi,
        pkt,
        jar,
    }
}