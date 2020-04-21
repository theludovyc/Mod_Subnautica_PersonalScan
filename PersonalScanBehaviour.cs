using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Straitjacket.Harmony;

namespace PersonalScan
{

	public class PersonalScanBehaviour : MonoBehaviour
	{
		/*private float mapScale
		{
			get
			{
				return this.hologramRadius / 500f;
			}
		}*/

		/*public static void GetMapRoomsInRange(Vector3 position, float range, ICollection<MapRoomFunctionality> outlist)
		{
			float num = range * range;
			for (int i = 0; i < MapRoomFunctionality.mapRooms.Count; i++)
			{
				MapRoomFunctionality mapRoomFunctionality = MapRoomFunctionality.mapRooms[i];
				if ((mapRoomFunctionality.transform.position - position).sqrMagnitude <= num)
				{
					outlist.Add(mapRoomFunctionality);
				}
			}
		}*/

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001DD08 File Offset: 0x0001BF08
		private void Start()
		{
			//this.wireFrameWorld.rotation = Quaternion.identity;

			//this.ReloadMapWorld();

			typeToScan = TechType.LimestoneChunk;


			if (this.typeToScan != TechType.None)
			{
				double num = this.timeLastScan;
				int num2 = this.numNodesScanned;
				this.StartScanning(this.typeToScan);
				this.timeLastScan = num;
				this.numNodesScanned = num2;
			}

			//base.GetComponentInParent<Base>().onPostRebuildGeometry += this.OnPostRebuildGeometry;

			ResourceTracker.onResourceDiscovered += this.OnResourceDiscovered;

			ResourceTracker.onResourceRemoved += this.OnResourceRemoved;

			/*this.matInstance = UnityEngine.Object.Instantiate<Material>(this.mat);
			this.matInstance.SetFloat(ShaderPropertyID._ScanIntensity, 0f);
			this.matInstance.SetVector(ShaderPropertyID._MapCenterWorldPos, base.transform.position);*/

			//MapRoomFunctionality.mapRooms.Add(this);

			//this.Subscribe(true);

			//this.powerRelay = base.GetComponentInParent<PowerRelay>();

			/*bool flag;

			if (this.powerRelay)
			{
				flag = (!GameModeUtils.RequiresPower() || this.powerRelay.IsPowered());
				this.prevPowerRelayState = flag;
				this.forcePoweredIfNoRelay = false;
			}
			else
			{
				flag = true;
				this.prevPowerRelayState = true;
				this.forcePoweredIfNoRelay = true;
			}*/

			//this.screenRoot.SetActive(true);
			
			//this.hologramRoot.SetActive(flag);
			
			/*if (flag)
			{
				this.ambientSound.Play();
			}*/
		}

		public void OnResourceDiscovered(ResourceTracker.ResourceInfo info)
		{
			if (this.typeToScan == info.techType && (transform.position - info.position).sqrMagnitude <= 250000f)
			{
				this.resourceNodes.Add(info);
			}
		}

		public void OnResourceRemoved(ResourceTracker.ResourceInfo info)
		{
			if (this.typeToScan == info.techType)
			{
				this.resourceNodes.Remove(info);
			}
		}

		public TechType GetActiveTechType()
		{
			return this.typeToScan;
		}

		/*private void OnPostRebuildGeometry(Base b)
		{
			Int3 @int = b.NormalizeCell(b.WorldToGrid(base.transform.position));
			Base.CellType cell = b.GetCell(@int);
			if (cell != Base.CellType.MapRoom && cell != Base.CellType.MapRoomRotated)
			{
				Debug.Log(string.Concat(new object[]
				{
				"map room had been destroyed, at cell ",
				@int,
				" new celltype is ",
				cell
				}));
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}*/

		/*public void ReloadMapWorld()
		{
			UnityEngine.Object.Destroy(this.mapWorld);
			base.StartCoroutine(this.LoadMapWorld());
		}*/

		/*private IEnumerator LoadMapWorld()
		{
			this.mapWorld = new GameObject("Map");
			this.mapWorld.transform.SetParent(this.wireFrameWorld, false);
			Int3 block = LargeWorldStreamer.main.GetBlock(base.transform.position);
			Int3 u = block - 500;
			Int3 u2 = block + 500;
			Int3 msCenterBlock = block >> this.mapLOD;
			Int3 mins = (u >> this.mapLOD) / this.mapChunkSize;
			Int3 maxs = (u2 >> this.mapLOD) / this.mapChunkSize;
			float chunkScale = this.mapScale * (float)(1 << this.mapLOD);
			Int3.RangeEnumerator iter = Int3.Range(mins, maxs);
			while (iter.MoveNext())
			{
				Int3 chunkId = iter.Current;
				string chunkPath = string.Format("WorldMeshes/Mini{0}/Chunk-{1}-{2}-{3}", new object[]
				{
				this.mapLOD,
				chunkId.x,
				chunkId.y,
				chunkId.z
				});
				ResourceRequest request = Resources.LoadAsync<Mesh>(chunkPath);
				yield return request;
				Mesh mesh = (Mesh)request.asset;
				if (mesh)
				{
					Int3 @int = chunkId * this.mapChunkSize - msCenterBlock;
					GameObject gameObject = new GameObject(chunkPath);
					gameObject.transform.SetParent(this.mapWorld.transform, false);
					gameObject.transform.localScale = new Vector3(chunkScale, chunkScale, chunkScale);
					gameObject.transform.localPosition = @int.ToVector3() * chunkScale;
					gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
					MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
					meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
					meshRenderer.sharedMaterial = this.matInstance;
					meshRenderer.receiveShadows = false;
					chunkId = default(Int3);
					chunkPath = null;
					request = null;
				}
			}
			iter = default(Int3.RangeEnumerator);
			yield break;
		}*/

		/*private bool CheckIsPowered()
		{
			return this.forcePoweredIfNoRelay || (this.powerRelay != null && this.powerRelay.IsPowered());
		}*/

		private void Update()
		{
			//ProfilingUtils.BeginSample("MapRoomFunctionality.Update()");

			//this.screenRoot.SetActive(true);

			this.timeLastScan = 0.0;

			this.UpdateScanning();

			this.ObtainResourceNodes(this.typeToScan);

			

			/*if (!this.forcePoweredIfNoRelay)
			{
				bool flag = this.CheckIsPowered();
				if (this.prevPowerRelayState && !flag)
				{
					this.screenRoot.SetActive(false);
					this.hologramRoot.SetActive(false);
					this.ambientSound.Stop();
				}
				else if (!this.prevPowerRelayState && flag)
				{
					
					this.hologramRoot.SetActive(true);
					
					this.ambientSound.Play();
				}
				this.prevPowerRelayState = flag;
			}
			
			if (this.modelUpdatePending)
			{
				this.UpdateModel();
				this.ObtainResourceNodes(this.typeToScan);
			}*/

			//ProfilingUtils.EndSample(null);
		}

		/*private void UpdateModel()
		{
			int count = this.storageContainer.container.count;
			for (int i = 0; i < this.upgradeSlots.Length; i++)
			{
				this.upgradeSlots[i].SetActive(i < count);
			}
			this.modelUpdatePending = false;
		}*/

		public float GetScanRange()
		{
			return 300f;

			//return Mathf.Min(500f, 300f + (float)this.storageContainer.container.GetCount(TechType.MapRoomUpgradeScanRange) * 50f);
		}

		public float GetScanInterval()
		{
			return 2f;

			//return Mathf.Max(1f, 14f - (float)this.storageContainer.container.GetCount(TechType.MapRoomUpgradeScanSpeed) * 3f);
		}

		private void ObtainResourceNodes(TechType typeToScan)
		{
			this.resourceNodes.Clear();

			Dictionary<string, ResourceTracker.ResourceInfo>.ValueCollection nodes = ResourceTracker.GetNodes(typeToScan);

			if (nodes != null)
			{
				float scanRange = this.GetScanRange();

				float num = scanRange * scanRange;

				foreach (ResourceTracker.ResourceInfo resourceInfo in nodes)
				{
					if ((transform.position - resourceInfo.position).sqrMagnitude <= num)
					{
						this.resourceNodes.Add(resourceInfo);
					}
				}
			}

			//ProfilingUtils.BeginSample("SortResourceNodes");

			this.resourceNodes.Sort(delegate (ResourceTracker.ResourceInfo a, ResourceTracker.ResourceInfo b)
			{
				float sqrMagnitude = (a.position - transform.position).sqrMagnitude;
				float sqrMagnitude2 = (b.position - transform.position).sqrMagnitude;
				return sqrMagnitude.CompareTo(sqrMagnitude2);
			});

			//ProfilingUtils.EndSample(null);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001E1C0 File Offset: 0x0001C3C0
		public void StartScanning(TechType newTypeToScan)
		{
			this.typeToScan = newTypeToScan;
			this.ObtainResourceNodes(this.typeToScan);
			this.mapBlips.Clear();
			UnityEngine.Object.Destroy(this.mapBlipRoot);
			this.mapBlipRoot = new GameObject("MapBlipRoot");
			this.mapBlipRoot.transform.SetParent(this.wireFrameWorld, false);
			this.scanActive = (this.typeToScan > TechType.None);
			this.numNodesScanned = 0;
			this.timeLastScan = 0.0;
		}

		public IList<ResourceTracker.ResourceInfo> GetNodes()
		{
			return this.resourceNodes;
		}

		public void GetDiscoveredNodes(ICollection<ResourceTracker.ResourceInfo> outNodes)
		{
			Debugger.Log("num : " + numNodesScanned + "res : " + resourceNodes.Count);

			int num = Mathf.Min(this.numNodesScanned, this.resourceNodes.Count);
			for (int i = 0; i < num; i++)
			{
				outNodes.Add(this.resourceNodes[i]);
			}
		}

		private void UpdateBlips()
		{
			if (this.scanActive)
			{
				int num = Mathf.Min(this.numNodesScanned + 1, this.resourceNodes.Count);
				if (num != this.numNodesScanned)
				{
					this.numNodesScanned = num;
				}
			}

			/*if (this.scanActive)
			{
				Vector3 position = this.mapBlipRoot.transform.position;
				int num = Mathf.Min(this.numNodesScanned + 1, this.resourceNodes.Count);
				if (num != this.numNodesScanned)
				{
					this.numNodesScanned = num;
				}
				for (int i = 0; i < num; i++)
				{
					Vector3 vector = (this.resourceNodes[i].position - position) * this.mapScale;
					
					if (i >= this.mapBlips.Count)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.blipPrefab, vector, Quaternion.identity);
						gameObject.transform.SetParent(this.mapBlipRoot.transform, false);
						this.mapBlips.Add(gameObject);
					}
					
					this.mapBlips[i].transform.localPosition = vector;
					
					this.mapBlips[i].SetActive(true);
				}
				for (int j = num; j < this.mapBlips.Count; j++)
				{
					this.mapBlips[j].SetActive(false);
				}
			}*/
		}

		/*private void UpdateCameraBlips()
		{
			float scanRange = this.GetScanRange();
			float num = scanRange * scanRange;
			Vector3 position = this.cameraBlipRoot.transform.position;
			int num2 = 0;
			for (int i = 0; i < MapRoomCamera.cameras.Count; i++)
			{
				Vector3 position2 = MapRoomCamera.cameras[i].transform.position;
				if ((this.wireFrameWorld.position - position2).sqrMagnitude <= num)
				{
					Vector3 vector = (position2 - position) * this.mapScale;
					if (num2 >= this.cameraBlips.Count)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cameraBlipPrefab, vector, Quaternion.identity);
						gameObject.transform.SetParent(this.cameraBlipRoot.transform, false);
						this.cameraBlips.Add(gameObject);
					}
					this.cameraBlips[num2].transform.localPosition = vector;
					this.cameraBlips[num2].SetActive(true);
					num2++;
				}
			}
			for (int j = num2; j < this.cameraBlips.Count; j++)
			{
				this.cameraBlips[j].SetActive(false);
			}
		}*/

		private void UpdateScanning()
		{
			DayNightCycle main = DayNightCycle.main;

			if (!main)
			{
				return;
			}

			double timePassed = main.timePassed;

			if (this.timeLastScan + (double)this.GetScanInterval() <= timePassed)
			{
				this.timeLastScan = timePassed;

				this.UpdateBlips();

				//this.UpdateCameraBlips();

				//float num = 1f / (this.GetScanRange() * this.mapScale);
				
				/*if (this.prevFadeRadius != num)
				{
					//this.matInstance.SetFloat(ShaderPropertyID._FadeRadius, num);
					this.prevFadeRadius = num;
				}*/
			}

			if (this.scanActive != this.prevScanActive)
			{
				//this.matInstance.SetFloat(ShaderPropertyID._ScanIntensity, this.scanActive ? 0.35f : 0f);
				this.prevScanActive = this.scanActive;
			}

			/*if (this.scanActive && this.powerRelay && this.timeLastPowerDrain + 1f < Time.time)
			{
				float num2;
				this.powerRelay.ConsumeEnergy(0.5f, out num2);
				this.timeLastPowerDrain = Time.time;
			}*/
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001E5F0 File Offset: 0x0001C7F0
		private void OnDestroy()
		{
			//UnityEngine.Object.Destroy(this.matInstance);

			/*Base componentInParent = base.GetComponentInParent<Base>();
			if (componentInParent)
			{
				componentInParent.onPostRebuildGeometry -= this.OnPostRebuildGeometry;
			}*/

			ResourceTracker.onResourceDiscovered -= this.OnResourceDiscovered;
			ResourceTracker.onResourceRemoved -= this.OnResourceRemoved;
			
			//MapRoomFunctionality.mapRooms.Remove(this);
		}

		/*private void Subscribe(bool state)
		{
			if (this.subscribed == state)
			{
				return;
			}
			if (this.subscribed)
			{
				this.storageContainer.container.onAddItem -= this.AddItem;
				this.storageContainer.container.onRemoveItem -= this.RemoveItem;
				this.storageContainer.container.isAllowedToAdd = null;
				this.storageContainer.container.isAllowedToRemove = null;
			}
			else
			{
				this.storageContainer.container.onAddItem += this.AddItem;
				this.storageContainer.container.onRemoveItem += this.RemoveItem;
				this.storageContainer.container.isAllowedToAdd = new IsAllowedToAdd(this.IsAllowedToAdd);
			}
			this.subscribed = state;
		}*/

		/*private void AddItem(InventoryItem item)
		{
			this.modelUpdatePending = true;
		}*/

		/*private void RemoveItem(InventoryItem item)
		{
			this.modelUpdatePending = true;
		}*/

		/*private bool IsAllowedToAdd(Pickupable pickupable, bool verbose)
		{
			TechType techType = pickupable.GetTechType();
			for (int i = 0; i < this.allowedUpgrades.Length; i++)
			{
				if (this.allowedUpgrades[i] == techType)
				{
					return true;
				}
			}
			return false;
		}*/

		private const int currentVersion = 1;

		[NonSerialized]
		public int version = 1;

		[NonSerialized]
		public int numNodesScanned;

		[NonSerialized]
		public TechType typeToScan;

		public Transform wireFrameWorld;

		public GameObject screenRoot;

		public GameObject hologramRoot;

		//public Material mat;

		public GameObject blipPrefab;

		public GameObject cameraBlipPrefab;

		public GameObject cameraBlipRoot;

		public StorageContainer storageContainer;

		//public GameObject[] upgradeSlots;

		//public FMOD_CustomLoopingEmitter ambientSound;

		public float hologramRadius = 1f;

		public int mapChunkSize = 32;

		public int mapLOD = 2;

		public const int mapScanRadius = 500;

		private const float defaultRange = 300f;

		private const float rangePerUpgrade = 50f;

		private const float baseScanTime = 14f;

		private const float scanTimeReductionPerUpgrade = 3f;

		private const float rotationTime = 50f;

		//private const float powerPerSecond = 0.5f;

		private GameObject mapWorld;

		private GameObject mapBlipRoot;

		private readonly List<ResourceTracker.ResourceInfo> resourceNodes = new List<ResourceTracker.ResourceInfo>();

		private readonly List<GameObject> mapBlips = new List<GameObject>();

		//private readonly List<GameObject> cameraBlips = new List<GameObject>();

		private double timeLastScan;

		private bool scanActive;

		private bool prevScanActive;

		//private float prevFadeRadius;

		//private Material matInstance;

		//private bool modelUpdatePending = true;

		//private static readonly List<MapRoomFunctionality> mapRooms = new List<MapRoomFunctionality>();

		//private bool subscribed;

		/*private readonly TechType[] allowedUpgrades = new TechType[]
		{
		TechType.MapRoomUpgradeScanRange,
		TechType.MapRoomUpgradeScanSpeed
		};*/

		//private bool forcePoweredIfNoRelay;

		//private bool prevPowerRelayState;

		//private PowerRelay powerRelay;

		//private float timeLastPowerDrain;
	}
}
