using System;
using System.Collections;
using System.Collections.Generic;

public class Compliant
{
    public System.Text.RegularExpressions.Regex Regex { get; } = new("a+"); // Compliant
    public System.Text.Encoding Encoding => System.Text.Encoding.UTF8; // Complaint 
}

