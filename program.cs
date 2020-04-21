using System;
using System.IO;
/** Class Program takes letter grade and hour information from a student file
 * and creates a new file displaying the student's name and GPA
 * @author Joseph Kern
 */
class Program
{
  public static void Main(string[] args)
  {
    StreamReader inFile = null;
    OpenFile(ref inFile);
    ProcessGrades(inFile, out string fullName, out float gpa);
    fileName.Close();
    StoreGpa(fullName, gpa);
    Console.WriteLine("Data processing complete");
  }

  /** OpenFile opens a selected .txt file to be edited.
   * @param fileName Name of the file to be opened
   * @pre Assumes the file is a .txt file
   */
  public static void OpenFile(ref StreamReader inFile)
  {
    try
    {
      Console.Write("Enter grade input file in the form filename.ext: ");
      inFile = new StreamReader(Console.ReadLine());
    }
    catch (Exception exc)
    {
      Console.WriteLine(exc.Message);
      Environment.Exit(1);
    }
  }

  private enum ConversionPoints
  {
    D = 1,
    C = 2,
    B = 3,
    A = 4
  }

  /** ProcessGrades reads information for the .txt file and stores the GPA
   * of the student.
   * @param fileName Name of the file to pull data from
   * @param fullName Name of the student in the record
   * @param gpa Calculated GPA of the student
   * @pre File is opened and ready for reading
   */
  public static void ProcessGrades(StreamReader inFile, out string fullName,
                                    out float gpa)
  {
    fullName = inFile.ReadLine();
    string record = null;
    string grade = null;
    float qualityPoints = 0;
    int hours = 0;
    int totalHours = 0;

    // Determine quality points to be assigned
    while ((grade = inFile.ReadLine()) != null)
    {
      hours = int.Parse(inFile.ReadLine());

      if (grade == "A")
      {
        qualityPoints += hours * (int)ConversionPoints.A;
      }
      else if (grade == "B")
      { 
        qualityPoints += hours * (int)ConversionPoints.B;
      }
      else if (grade == "C")
      {
        qualityPoints += hours * (int)ConversionPoints.C;
      }
      else if (grade == "D")
      {
        qualityPoints += hours * (int)ConversionPoints.D;
      }
     
      // Determine total hours
      totalHours += hours;

    }

    gpa = qualityPoints / (float)totalHours;
  }

  /** StoreGpa takes information and places it in a new .txt file.
   * @param fullName Full name of the student
   * @param gpa Calculated GPA of the student
   */
  public static void StoreGpa(string fullName, float gpa)
  {
    StreamWriter outFile = null;
    try
    {
      outFile = new StreamWriter(fullName + ".txt");
    }
    catch (Exception exc)
    {
      Console.WriteLine(exc.Message);
      Environment.Exit(2);
    }
    outFile.WriteLine(fullName);
    outFile.WriteLine($"{gpa:F2}");
    outFile.Close();
  }
}
