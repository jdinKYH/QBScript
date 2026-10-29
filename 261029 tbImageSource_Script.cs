using System;
using Cognex.VisionPro;
using Cognex.VisionPro3D;
using Cognex.VisionPro.ToolGroup;

public class UserScript : CogToolGroupBaseScript
{

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

    // Run each tool in the tool group using the RunTool function
    for (Int32 toolIdx = 0; toolIdx < toolGroup.Tools.Count; toolIdx++)
      toolGroup.RunTool(toolGroup.Tools[toolIdx], ref message, ref result);

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

