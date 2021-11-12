class Controller
{
    bool Accéss() // Compliant {{'é' might be not desired, but is not a Trojan Horse.}}
    {
        string access_level = "user";
        if (access_level != "⁣user") // Noncompliant {{Trojan Horse character U+2063 detected.}}
        //                   ^
        {
            return true;
        }
        return false;
    }

    string RumiNumeral5()
    {
        return "𐹤" + "⁣"; // Noncompliant
        //            ^
    }
}
