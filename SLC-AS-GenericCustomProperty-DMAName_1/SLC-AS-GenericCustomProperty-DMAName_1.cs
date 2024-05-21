namespace SLC_AS_GenericCustomProperty_DMAName_1
{
	using System.Linq;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{
		/// <summary>
		/// The script entry point.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		public void Run(IEngine engine)
		{
			string propertyName = engine.GetScriptParam("Property Name").Value;

			var dms = engine.GetDms();
			var dmas = dms.GetAgents();

			if (!dms.PropertyExists(propertyName, PropertyType.Element))
				dms.CreateProperty(propertyName, PropertyType.Element, true, false, false);

			foreach (var element in engine.FindElements(ElementFilter.ByName("*")))
			{
				element.SetPropertyValue(propertyName, dmas.FirstOrDefault(agent => agent.Id == element.DmaId).Name);
			}
		}
	}
}