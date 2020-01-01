using NPOI.DDF;
using System.Collections;

namespace NPOI.HSSF.Model
{
	/// <summary>
	/// Provides utilities to manage drawing Groups.
	/// </summary>
	/// <remarks>
	/// Glen Stampoultzis (glens at apache.org) 
	/// </remarks>
	public class DrawingManager2
	{
		private EscherDggRecord dgg;

		private IList drawingGroups = new ArrayList();

		public DrawingManager2(EscherDggRecord dgg)
		{
			this.dgg = dgg;
		}

		/// <summary>
		/// Clears the cached list of drawing Groups
		/// </summary>
		public void ClearDrawingGroups()
		{
			drawingGroups.Clear();
		}

		public virtual EscherDgRecord CreateDgRecord()
		{
			EscherDgRecord escherDgRecord = new EscherDgRecord();
			escherDgRecord.RecordId = -4088;
			short num = FindNewDrawingGroupId();
			escherDgRecord.Options = (short)(num << 4);
			escherDgRecord.NumShapes = 0;
			escherDgRecord.LastMSOSPID = -1;
			drawingGroups.Add(escherDgRecord);
			dgg.AddCluster(num, 0);
			dgg.DrawingsSaved += 1;
			return escherDgRecord;
		}

		/// <summary>
		/// Allocates new shape id for the new drawing Group id.
		/// </summary>
		/// <param name="drawingGroupId"></param>
		/// <returns>a new shape id.</returns>
		public virtual int AllocateShapeId(short drawingGroupId)
		{
			EscherDgRecord drawingGroup = GetDrawingGroup(drawingGroupId);
			return AllocateShapeId(drawingGroupId, drawingGroup);
		}

		/// <summary>
		/// Allocates new shape id for the new drawing group id.
		/// </summary>
		/// <param name="drawingGroupId"></param>
		/// <param name="dg"></param>
		/// <returns>a new shape id.</returns>
		public virtual int AllocateShapeId(short drawingGroupId, EscherDgRecord dg)
		{
			dgg.NumShapesSaved += 1;
			for (int i = 0; i < dgg.FileIdClusters.Length; i++)
			{
				EscherDggRecord.FileIdCluster fileIdCluster = dgg.FileIdClusters[i];
				if (fileIdCluster.DrawingGroupId == drawingGroupId && fileIdCluster.NumShapeIdsUsed != 1024)
				{
					int num = fileIdCluster.NumShapeIdsUsed + 1024 * (i + 1);
					fileIdCluster.IncrementShapeId();
					dg.NumShapes++;
					dg.LastMSOSPID = num;
					if (num >= dgg.ShapeIdMax)
					{
						dgg.ShapeIdMax = num + 1;
					}
					return num;
				}
			}
			dgg.AddCluster(drawingGroupId, 0);
			dgg.FileIdClusters[dgg.FileIdClusters.Length - 1].IncrementShapeId();
			dg.NumShapes++;
			int num3 = dg.LastMSOSPID = 1024 * (int)dgg.FileIdClusters.LongLength;
			if (num3 >= dgg.ShapeIdMax)
			{
				dgg.ShapeIdMax = num3 + 1;
			}
			return num3;
		}

		/// <summary>
		/// Finds the next available (1 based) drawing Group id
		/// </summary>
		/// <returns></returns>
		public short FindNewDrawingGroupId()
		{
			short num = 1;
			while (DrawingGroupExists(num))
			{
				num = (short)(num + 1);
			}
			return num;
		}

		private EscherDgRecord GetDrawingGroup(int drawingGroupId)
		{
			return (EscherDgRecord)drawingGroups[drawingGroupId - 1];
		}

		private bool DrawingGroupExists(short dgId)
		{
			for (int i = 0; i < dgg.FileIdClusters.Length; i++)
			{
				if (dgg.FileIdClusters[i].DrawingGroupId == dgId)
				{
					return true;
				}
			}
			return false;
		}

		private int FindFreeSPIDBlock()
		{
			int shapeIdMax = dgg.ShapeIdMax;
			return (shapeIdMax / 1024 + 1) * 1024;
		}

		public EscherDggRecord GetDgg()
		{
			return dgg;
		}

		public void IncrementDrawingsSaved()
		{
			dgg.DrawingsSaved += 1;
		}
	}
}
