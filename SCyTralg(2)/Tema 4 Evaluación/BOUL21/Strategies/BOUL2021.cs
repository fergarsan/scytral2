#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.Indicators;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Strategies in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Strategies
{
	public class BOUL2021 : Strategy
	{
	/// Tipo: Sistema de pauta horaria nocturna
	/// Plantilla horaria: 24/7 Madrid
	/// TF de referencia: 30 min.
	/// Mercado de referencia: ES
	/// Mercados alternativos: NQ, YM, EMD, RTY
	/// Sistema con finalidad educativa diseñado para el curso UPM 2017-2018
    
		// Variables Internas
		private bool Tendencia;
		private bool Pullback;
		private double CloseToday, CloseYesterday; 
		
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Sistema de pauta horaria nocturna";
				Name										= "BOUL2021";
				Calculate									= Calculate.OnBarClose;
				EntriesPerDirection							= 1;
				EntryHandling								= EntryHandling.AllEntries;
				IsExitOnSessionCloseStrategy				= true;
				ExitOnSessionCloseSeconds					= 30;
				IsFillLimitOnTouch							= false;
				MaximumBarsLookBack							= MaximumBarsLookBack.TwoHundredFiftySix;
				OrderFillResolution							= OrderFillResolution.Standard;
				Slippage									= 0;
				StartBehavior								= StartBehavior.WaitUntilFlat;
				TimeInForce									= TimeInForce.Gtc;
				TraceOrders									= false;
				RealtimeErrorHandling						= RealtimeErrorHandling.StopCancelClose;
				StopTargetHandling							= StopTargetHandling.PerEntryExecution;
				BarsRequiredToTrade							= 20;
				// Disable this property for performance gains in Strategy Analyzer optimizations
				// See the Help Guide for additional information
				IsInstantiatedOnEachOptimizationIteration	= false;
				StopTrendUp					= 1.7;
				StopTrendDown				= 0.8;
				SMAPeriod					= 1300;
				CMOPeriod					= 7;
				CMOLevel					=12;
				NTicks						=7;
			
				
			}
			else if (State == State.Configure)
			{
				AddChartIndicator(SMA(SMAPeriod));
				AddChartIndicator(CMO(CMOPeriod));
			}
		}

		protected override void OnBarUpdate()
		{
			// COMPROBAMOS QUE HAY SUFICIENTES BARRAS
			if (CurrentBar < 50) return;
			
			// SALIDAS HORARIAS
			int ETD = 93000; // Fin de la Pauta
			int ETU = 150000; // Pauta Extendida
			
			
			
			{
				if (Close[0]+NTicks*TickSize < PriorDayOHLC().PriorOpen[0] )
				{
					Pullback = true;
					BackBrushAll = Brushes.PaleGreen;
				}	
				else
				{	
					Pullback = false;
					BackBrushAll = Brushes.Pink;
				}	
			}	
			
			// SI ESTAMOS FLAT
			if (Position.MarketPosition == MarketPosition.Flat 
				&& Bars.IsFirstBarOfSession
				&& Time[0].DayOfWeek != DayOfWeek.Sunday
				&& CMO(CMOPeriod)[0]<CMOLevel
				&& Pullback == true
				
			
			
				
				
				)		
        	{
            	if(Close[0] > SMA(SMAPeriod)[0])
				{
					Tendencia = true;
					EnterLong(1, "Enter Up Trend");
					SetStopLoss("", CalculationMode.Percent, StopTrendUp/100, false);
					
				}
				else 
				{
					Tendencia = false;
					EnterLong(1, "Enter Down Trend");
					SetStopLoss("", CalculationMode.Percent, StopTrendDown/100, false);
						
				}			
				
			}
			
			// SI ESTAMOS LARGOS
			if (Position.MarketPosition == MarketPosition.Long)			
			{
				if(Tendencia)
				{
					if (ToTime(Time[0])>= ETU)
					{
						ExitLong("Exit Up Trend", "");						
					}		
				}
				else					
					if (ToTime(Time[0])>= ETD)
					{
						ExitLong("Exit Down Trend", "");
					}		
			}
			
		}

		#region Properties
		[NinjaScriptProperty]
		[Range(0, double.MaxValue)]
		[Display(Name="StopTrendUp", Order=1, GroupName="Parameters")]
		public double StopTrendUp
		{ get; set; }
		[NinjaScriptProperty]
		[Range(0, double.MaxValue)]
		[Display(Name="StopTrendDown", Order=2, GroupName="Parameters")]
		public double StopTrendDown
		{ get; set; }

		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="SMAPeriod", Order=3, GroupName="Parameters")]
		public int SMAPeriod
		{ get; set; }
		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="CMOPeriod", Order=4, GroupName="Parameters")]
		public int CMOPeriod
		{ get; set; }
		[NinjaScriptProperty]
		[Range(-100, double.MaxValue)]
		[Display(Name="CMOLevel", Order=4, GroupName="Parameters")]
		public double CMOLevel
		{ get; set; }
		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="NTicks", Order=3, GroupName="Parameters")]
		public int NTicks
		{ get; set; }
		
		
		#endregion

	}
}
