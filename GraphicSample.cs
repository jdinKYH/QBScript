// CogCircle dC1;
// CogCircle dC2;
// CogLineSegment dSeg;

// Array List
CogGraphicCollection cgc = new CogGraphicCollection();

public override bool GroupRun(ref string message, ref CogToolResultConstants result)
{

  cgc.Clear();

  CogFindCircleTool mC1 = mToolBlock.Tools["CogFindCircleTool1"] as CogFindCircleTool;
  CogFindCircleTool mC2 = mToolBlock.Tools["CogFindCircleTool2"] as CogFindCircleTool;
  CogDistancePointPointTool mDis = mToolBlock.Tools["CogDistancePointPointTool1"] as CogDistancePointPointTool;
  CogIntersectLineLineTool mInter = mToolBlock.Tools["CogIntersectLineLineTool1"] as CogIntersectLineLineTool;
  CogBlobTool mBlob = mToolBlock.Tools["CogBlobTool1"] as CogBlobTool;


  // Run each tool using the RunTool function
  foreach(ICogTool tool in mToolBlock.Tools)
    mToolBlock.RunTool(tool, ref message, ref result);

  double c1x = mC1.Results.GetCircle().CenterX;
  double c1y = mC1.Results.GetCircle().CenterY;
  double c2x = mC2.Results.GetCircle().CenterX;
  double c2y = mC2.Results.GetCircle().CenterY;

  //CIRCLE
  CogCircle dC1 = mC1.Results.GetCircle();
  dC1.Color = CogColorConstants.Yellow;
  dC1.LineWidthInScreenPixels = 3;

  dC2 = mC2.Results.GetCircle();
  dC2.Color = CogColorConstants.Yellow;
  dC2.LineWidthInScreenPixels = 3;

  //LINE SEGMENT
  CogLineSegment dSeg = new CogLineSegment();
  dSeg.StartX = c1x;
  dSeg.StartY = c1y;
  dSeg.EndX = c2x;
  dSeg.EndY = c2y;
  dSeg.Color = CogColorConstants.Yellow;
  dSeg.LineWidthInScreenPixels = 3;
  dSeg.SelectedSpaceName = "Fixture";
  dSeg.StartPointAdornment = CogLineSegmentAdornmentConstants.Arrow;
  dSeg.EndPointAdornment = CogLineSegmentAdornmentConstants.SolidArrow;
  cgc.Add(dSeg);

  //POINT
  CogPointMarker dPoint = new CogPointMarker();  
  dPoint.X = (c1x + c2x) / 2;
  dPoint.Y = (c1y + c2y) / 2;
  dPoint.Color = CogColorConstants.Red;
  dPoint.LineWidthInScreenPixels = 5;
  dPoint.SelectedSpaceName = "Fixture";
  cgc.Add(dPoint);

  //LABEL
  CogGraphicLabel dDis = new CogGraphicLabel();
  dDis.X = dPoint.X;
  dDis.Y = dPoint.Y - 5;
  dDis.Text = "Distance: " + mDis.Distance.ToString("F2") + " pixels";
  dDis.Font = new Font("Arial", 14);
  dDis.Color = CogColorConstants.Yellow;
  dDis.BackgroundColor = CogColorConstants.Black;
  dDis.SelectedSpaceName = "Fixture";
  dDis.Alignment = CogGraphicLabelAlignmentConstants.BottomCenter;
  cgc.Add(dDis);

  //COORDINATE AXES
  CogCoordinateAxes dAxes = new CogCoordinateAxes();
  dAxes.OriginX = mInter.X;
  dAxes.OriginY = mInter.Y;
  dAxes.Color = CogColorConstants.Red;
  dAxes.SelectedSpaceName = "Fixture";
  dAxes.LineWidthInScreenPixels = 3;
  dAxes.DisplayedXAxisLength = 50;
  dAxes.DisplayedAspect = 2;
  dAxes.XAxisEndPointAdornment = CogCoordinateAxesEndPointAdornmentConstants.SolidArrow);
  cgc.Add(dAxes);

  //RECTANGLE REGION
  CogRectangle dRect = (CogRectangle) mBlob.Region;
  dRect.SelectedSpaceName = "Fixture";
  cgc.Add(dRect);


  //RECTANGLE REGION
  CogRectangle dRect = (CogRectangle) mBlob.Region;
  dRect.SelectedSpaceName = "Fixture";
  cgc.Add(dRect);

  //POLYGON
  CogPolygon dPoly = mBlob.Results.GetBlobs()[0].GetBoundary();
  dPoly.SelectedSpaceName = "Fixture";
  dPoly.Color = CogColorConstants.Yellow;
  dPoly.LineWidthInScreenPixels = 3;
  cgc.Add(dPoly);

  //LABEL
  CogGraphicLabel dArea = dDis.Copy(CogCopyShapeConstants.All);
  dArea.X = dRect.X;
  dArea.Y = dRect.Y;
  dArea.Text = "Area: " + mBlob.Results.GetBlobs()[0].Area.ToString("F2") + " pixels";
  cgc.Add(dArea);

  //LABEL
  CogGraphicLabel dArea = dDis.Copy(CogCopyShapeConstants.All);
  dArea.SetXYText(dRect.X, dRect.Y, "Area: " + mBlob.Results.GetBlobs()[0].Area.ToString("F2") + " pixels");
  dArea.Alignment = CogGraphicLabelAlignmentConstants.BottomLeft;
  cgc.Add(dArea);


  return false;
}



#region When the Last Run Record is Created
/// <summary>
/// Called when the last run record may have changed and is being reconstructed
/// </summary>
/// <param name="lastRecord">
/// The new last run record is available to be initialized or customized.</param>
public override void ModifyLastRunRecord(Cognex.VisionPro.ICogRecord lastRecord)
{
  // mToolBlock.AddGraphicToRunRecord(dC1, lastRecord, lastRecord.SubRecords[0].RecordKey, "script");
  // mToolBlock.AddGraphicToRunRecord(dC2, lastRecord, lastRecord.SubRecords[0].RecordKey, "script");
  // mToolBlock.AddGraphicToRunRecord(dSeg, lastRecord, lastRecord.SubRecords[0].RecordKey, "script");
  // mToolBlock.AddGraphicToRunRecord(dPoint, lastRecord, lastRecord.SubRecords[0].RecordKey, "script");

  // Use Array List 
  foreach(ICogGrapic cg in cgc)
  {
    mToolBlock.AddGraphicToRunRecord(cg, lastRecord, lastRecord.SubRecords[0].RecordKey, "script");
  }
}
#endregion