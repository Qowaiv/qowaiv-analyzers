global using System.Collections; // Noncompliant ^0#32 {{Define global using statement in a separate file.}}
global using static System.Math; // Noncompliant
global using TXT = System.Text; //  Noncompliant
using System.Collections.Generic;

public class Other { }
