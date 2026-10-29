#region namespace imports
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro3D;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.Dimensioning;
#endregion

public class CogToolBlockAdvancedScript : CogToolBlockAdvancedScriptBase
{
  #region Private Member Variables
  private Cognex.VisionPro.ToolBlock.CogToolBlock mToolBlock;
  #endregion
  // Array List
  CogGraphicCollection cgc = new CogGraphicCollection();
  
  /// <summary>
  /// Called when the parent tool is run.
  /// Add code here to customize or replace the normal run behavior.
  /// </summary>
  /// <param name="message">Sets the Message in the tool's RunStatus.</param>
  /// <param name="result">Sets the Result in the tool's RunStatus</param>
  /// <returns>True if the tool should run normally,
  ///          False if GroupRun customizes run behavior</returns>
  public override bool GroupRun(ref string message, ref CogToolResultConstants result)
  {
    // To let the execution stop in this script when a debugger is attached, uncomment the following lines.
    // #if DEBUG
    // if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
    // #endif


    cgc.Clear();

    CogCaliperTool mC1 = mToolBlock.Tools["CogCaliperTool1"] as CogCaliperTool;  
    CogCaliperTool mC2 = mToolBlock.Tools["CogCaliperTool2"] as CogCaliperTool;
    
    CogDistancePointPointTool mDist1 = mToolBlock.Tools["CogDistancePointPointTool1"] as CogDistancePointPointTool;
    CogDistancePointPointTool mDist2 = mToolBlock.Tools["CogDistancePointPointTool2"] as CogDistancePointPointTool;
    
    // Run each tool using the RunTool function
    foreach(ICogTool tool in mToolBlock.Tools)
      mToolBlock.RunTool(tool, ref message, ref result);
    
    
    // ====================================
    //               Upper
    // ====================================
    
    // LINE SEGMENT
    CogLineSegment dSeg = new CogLineSegment();
    dSeg.StartX = mDist1.StartX;
    dSeg.StartY = mDist1.StartY;
    dSeg.EndX = mDist1.EndX;
    dSeg.EndY = mDist1.EndY;
    dSeg.Color = CogColorConstants.Green;
    dSeg.LineWidthInScreenPixels = 2;
    dSeg.SelectedSpaceName = "Fixture";
    dSeg.StartPointAdornment = CogLineSegmentAdornmentConstants.Arrow;
    dSeg.EndPointAdornment = CogLineSegmentAdornmentConstants.SolidArrow;
    cgc.Add(dSeg);
    
    
    //LABEL #1
    CogGraphicLabel dDis1 = new CogGraphicLabel();
    dDis1.X = mDist1.StartX;
    dDis1.Y = mDist1.StartY; // +(mDist1.StartY - mDist1.EndY) / 2 * -1;
    dDis1.Text = "Distance R1: " + mDist1.Distance.ToString("F2") + " pixels";
    dDis1.Font = new Font("Arial", 14);
    dDis1.Color = CogColorConstants.Green;
    dDis1.BackgroundColor = CogColorConstants.Black;
    dDis1.SelectedSpaceName = "Fixture";
    dDis1.Alignment = CogGraphicLabelAlignmentConstants.BottomCenter;
    cgc.Add(dDis1);    
    

    // ====================================
    //                Lower 
    // ====================================

    // LINE SEGMENT
    CogLineSegment dSeg2 = new CogLineSegment();
    dSeg2.StartX = mDist2.StartX;
    dSeg2.StartY = mDist2.StartY;
    dSeg2.EndX = mDist2.EndX;
    dSeg2.EndY = mDist2.EndY;
    dSeg2.Color = CogColorConstants.Green;
    dSeg2.LineWidthInScreenPixels = 2;
    dSeg2.SelectedSpaceName = "Fixture";
    dSeg2.StartPointAdornment = CogLineSegmentAdornmentConstants.Arrow;
    dSeg2.EndPointAdornment = CogLineSegmentAdornmentConstants.SolidArrow;
    cgc.Add(dSeg2);
    
    //LABEL #2
    CogGraphicLabel dDis2 = new CogGraphicLabel();
    dDis2.X = mDist2.StartX;
    dDis2.Y = mDist2.StartY; // +mDist2.Distance + 75; // +(mDist2.StartY + mDist2.EndY) / 2 * -1;    
    dDis2.Text = "Distance R2: " + mDist2.Distance.ToString("F2") + " pixels";
    dDis2.Font = new Font("Arial", 14);
    dDis2.Color = CogColorConstants.Green;
    dDis2.BackgroundColor = CogColorConstants.Black;
    dDis2.SelectedSpaceName = "Fixture";
    dDis2.Alignment = CogGraphicLabelAlignmentConstants.BottomCenter;
    cgc.Add(dDis2);  
    
    return false;
  }

  #region When the Current Run Record is Created
  /// <summary>
  /// Called when the current record may have changed and is being reconstructed
  /// </summary>
  /// <param name="currentRecord">
  /// The new currentRecord is available to be initialized or customized.</param>
  public override void ModifyCurrentRunRecord(Cognex.VisionPro.ICogRecord currentRecord)
  {
  }
  #endregion

  #region When the Last Run Record is Created
  /// <summary>
  /// Called when the last run record may have changed and is being reconstructed
  /// </summary>
  /// <param name="lastRecord">
  /// The new last run record is available to be initialized or customized.</param>
  public override void ModifyLastRunRecord(Cognex.VisionPro.ICogRecord lastRecord)
  {
    // 
    // mToolBlock.AddGraphicToRunRecord(dC1, lastRecord, lastRecord.SubRecords[0].RecordKey, "script");
    // mToolBlock.AddGraphicToRunRecord(dC2, lastRecord, lastRecord.SubRecords[0].RecordKey, "script");
    // mToolBlock.AddGraphicToRunRecord(dSeg, lastRecord, lastRecord.SubRecords[0].RecordKey, "script");
    // mToolBlock.AddGraphicToRunRecord(dPoint, lastRecord, lastRecord.SubRecords[0].RecordKey, "script");
    
    // Use Array List 
    foreach(ICogGraphic cg in cgc)
    {
      mToolBlock.AddGraphicToRunRecord(cg, lastRecord, lastRecord.SubRecords[0].RecordKey, "script");
    }
  }
  #endregion

  #region When the Script is Initialized
  /// <summary>
  /// Perform any initialization required by your script here
  /// </summary>
  /// <param name="host">The host tool</param>
  public override void Initialize(Cognex.VisionPro.ToolGroup.CogToolGroup host)
  {
    // DO NOT REMOVE - Call the base class implementation first - DO NOT REMOVE
    base.Initialize(host);


    // Store a local copy of the script host
    this.mToolBlock = ((Cognex.VisionPro.ToolBlock.CogToolBlock)(host));
  }
  #endregion

}


