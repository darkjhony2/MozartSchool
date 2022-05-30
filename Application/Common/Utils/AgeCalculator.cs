namespace ColegioMozart.Application.Common.Utils;

public static class AgeCalculator
{

    public static int GetAge(this DateTime birthdate)
    {
        var today = DateTime.Today;

        var a = (today.Year * 100 + today.Month) * 100 + today.Day;
        var b = (birthdate.Year * 100 + birthdate.Month) * 100 + birthdate.Day;

        return (a - b) / 10000;
    }

}
