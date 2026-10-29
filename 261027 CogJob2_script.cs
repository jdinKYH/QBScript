using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro3D;
using Cognex.VisionPro.ToolGroup;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.Dimensioning;


public class UserScript : CogToolGroupBaseScript
{

  // Array List
  CogGraphicCollection cgc = new CogGraphicCollection();
  
  // The GroupRun function is called when the tool group is run.  The default
  // implementation provided here is equivalent to the normal behavior of the
  // tool group.  Modifying this function will allow you to change the behavior
  // when the tool group is run.
  public override bool GroupRun(ref string message, ref CogToolResultConstants result)
  {
    // To let the execution stop in this script when a debugger is attached, uncomment the following lines.
    // #if DEBUG
    // if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
    // #endif

    cgc.Clear();
    
    CogToolBlock mLocateTB = (CogToolBlock)toolGroup.Tools["Locate"];
    CogToolBlock mLEFTsideTB = (CogToolBlock)toolGroup.Tools["LEFTside"];
    CogToolBlock mRIGHTB = (CogToolBlock)toolGroup.Tools["RIGHTside"];
    
    // Run each tool in the tool group using the RunTool function
    for (Int32 toolIdx = 0; toolIdx < toolGroup.Tools.Count; toolIdx++)
      toolGroup.RunTool(toolGroup.Tools[toolIdx], ref message, ref result);


    // Example using a segment to spawn a line object
    CogLineSegment mySegment = new CogLineSegment();
    mySegment.Color = CogColorConstants.Red;
    mySegment.LineWidthInScreenPixels = 8;
    mySegment.SelectedSpaceName = "Fixture";
    mySegment.SetStartEnd(-100, -100, -500, -500);
    cgc.Add(mySegment);
    
    // CogCreateLine equivalent via the CreateLine method
   　CogLine calculatedLine = mySegment.CreateLine(); 
    // Generates an infinite line leveraging the segment's midpoint as a reference
    
    
    //LABEL #1
    CogGraphicLabel dDis1 = new CogGraphicLabel();
    dDis1.X = 100;  // Fixture 기준 좌표
    dDis1.Y = -200; // Fixture 기준 좌표    
    dDis1.Text = "Distance: " + "-----" + " pixels";
    dDis1.Font = new Font("Arial", 14);
    dDis1.Color = CogColorConstants.Yellow;
    dDis1.BackgroundColor = CogColorConstants.Black;
    dDis1.SelectedSpaceName = "Fixture";
    dDis1.Alignment = CogGraphicLabelAlignmentConstants.BottomCenter;
    cgc.Add(dDis1);   
    
    // Returning False indicates we ran the tools in script, and they should not be
    // run by VisionPro 
    return false;
  }

#region "When the Current Run Record is Created"
  public override void ModifyCurrentRunRecord(Cognex.VisionPro.ICogRecord currentRecord)
  {
  }
#endregion

#region "When the Last Run Record is Created"
  // Allows you to add or modify the contents of the last run record when it is
  // created.  For example, you might add custom graphics to the run record here.
  public override void ModifyLastRunRecord(Cognex.VisionPro.ICogRecord lastRecord)
  {

    // Use Array List 
    // 261027@jdin : 
    // ToolGroup Grphics은 LastRun.Image.Source.OutputImage에 나타난다. - 0
    // 각 ToolBlock Grphics은 LastRun.Locate.CogFixtureTool.OutputImage. 나타난다 - 3
    //
    foreach(ICogGraphic cg in cgc)
    {
      toolGroup.AddGraphicToRunRecord(cg, lastRecord, lastRecord.SubRecords[0].RecordKey, "script");
    }
  }
#endregion

#region "When the Script is Initialized"
  // Perform any initialization required by your script here
  public override void Initialize(CogToolGroup host)
  {
    // DO NOT REMOVE - Call the base class implementation first - DO NOT REMOVE
    base.Initialize(host);
  }
#endregion

}

