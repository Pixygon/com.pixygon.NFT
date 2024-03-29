using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.Video;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Pixygon.NFT {
    public class NFTObject : MonoBehaviour {
        //, IInteractable {
        [SerializeField] private NftActionsList _actionsList;
        [SerializeField] protected Transform _uiObjectParent;
        [SerializeField] protected Transform _parent;
        public Transform _root;
        [SerializeField] protected AudioMixerGroup _audioMixer;
        [SerializeField] protected ParticleSystem _mintPS;
        [SerializeField] protected GameObject _decalPrefab;
        [SerializeField] protected GameObject _videoPrefab;
        [SerializeField] protected Component _interactObject;
        [SerializeField] private Material _previewMaterial;
        [SerializeField] private BoxCollider _triggerCollider;
        [SerializeField] private GameObject _spriteBase;

        private NavMeshAgent _agent;
        private Transform _player;
        //private PhotonView _photonView;
        private int _moveTimer;
        public GameObject _nft;
        private bool _isWalking;

        private bool _initialized;
        private bool _notInteractable;
        private bool _isPreview;

        public bool NotInteractable => _notInteractable;
        //public Tool RequiredTool => Tool.None;
        public int RequiredToolLevel => 0;
        public Transform Root => _root;
        public NftTemplateObject NftTemplate { get; private set; }

        public float Size;
        public bool IsFurniture { get; private set; }

        [SerializeField] private GameObject _spawnFX;

        public List<NftActionAsset> actions = new List<NftActionAsset>();


        public virtual void Initialize(NftTemplateObject a, Transform player, float size, bool isFurniture = false,
            List<string> actionAssets = null) {
            NftTemplate = a;
            _player = player;
            IsFurniture = isFurniture;


            //if (NFTAsset.Data.nsfw && !SettingsSubsystem.instance.SettingsData.NSFW) {
            //    Destroy(this.gameObject);
            //    return;
            //}

            //if (!IsFurniture)
            //    gameObject.AddComponent<PhotonTransformView>();
            Size = size;
            SetNFTType();
            //_notInteractable = NFTAsset.Data.nftAction == NFTAction.FollowAnimated;
            if (_notInteractable) {
                Debug.Log("Destroy da componant");
                Destroy(_interactObject);
            }

            if (actionAssets != null)
                AddActions(actionAssets);

            _initialized = true;
            if (_spawnFX) _spawnFX.SetActive(!isFurniture);
        }


        public async virtual void InitializePreview(NftTemplateObject a, Transform player, float size) {
            _isPreview = true;
            GetComponent<BoxCollider>().enabled = false;
            
            NftTemplate = a;
            _player = player;
            //if (NFTAsset.Data.nsfw && !SettingsSubsystem.instance.SettingsData.NSFW) {
            //    Destroy(gameObject);
            //    return;
            //}

            switch(NftTemplate.MediaType) {
                case NFTType.Image:
                //if(NFTAsset.Data.nftAction == NFTAction.Randomize) {
                //    _nft = await AddressableLoader.LoadGameObject(NFTAsset.Data.modelRef, _uiObjectParent);
                //    _nft.transform.localRotation = Quaternion.identity;
                //    _nft.transform.localScale = Vector3.one;
                //} else {
                        _nft = Instantiate(_spriteBase, _uiObjectParent);
                        _nft.transform.localRotation = Quaternion.identity;
                        _nft.transform.localScale = Vector3.one;
                        _nft.GetComponent<Image>().color = new Color(.2f, .7f, 1f, .5f);
                        _nft.GetComponent<Image>().sprite = await NFT.GetImageFromIpfs(NftTemplate.IpfsHashes[0]);
                    //_nft = await AddressableLoader.LoadGameObject(NFTAsset.Data.prefabRef, _uiObjectParent);
                    //_nft.transform.localRotation = Quaternion.identity;
                    //_nft.transform.localScale = Vector3.one;
                //}
                _nft.GetComponent<Image>().color = new Color(.2f, .7f, 1f, .5f);
                break;
                case NFTType.Model:
                //_nft = await AddressableLoader.LoadGameObject(NFTAsset.Data.modelRef, _parent);
                //_nft.transform.localRotation = Quaternion.identity;
                //_nft.transform.localScale = Vector3.one;
                SetPreviewMaterial();
                break;
                case NFTType.Book:
                //_nft = await AddressableLoader.LoadGameObject(NFTAsset.Data.modelRef, _parent);
                //_nft.transform.localRotation = Quaternion.identity;
                //_nft.transform.localScale = Vector3.one;
                SetPreviewMaterial();
                var colliders = _nft.GetComponentsInChildren<Collider>();
                foreach(var col in colliders) {
                    col.enabled = false;
                }
                break;
                case NFTType.Audio:
                //_nft = await AddressableLoader.LoadGameObject(NFTAsset.Data.prefabRef, _uiObjectParent);
                //_nft.transform.localRotation = Quaternion.identity;
                //_nft.transform.localScale = Vector3.one;
                //_nft.GetComponent<UnityEngine.UI.Image>().color = new Color(.2f, .7f, 1f, .5f);
                break;

                case NFTType.Video:
                //_nft = Instantiate(_videoPrefab, _parent);
                //_nft.GetComponent<VideoPlayer>().clip = NFTAsset.Data.video;
                //_nft.GetComponent<VideoPlayer>().Play();
                //_nft.GetComponent<VideoPlayer>().aspectRatio = VideoAspectRatio.FitVertically;
                //Vector2 scale = new Vector2((float)_nft.GetComponent<VideoPlayer>().clip.width,
                //    (float)_nft.GetComponent<VideoPlayer>().clip.height);
                //scale.Normalize();
                //_nft.GetComponent<VideoPlayer>().targetMaterialRenderer.transform.localScale = scale;
                break;
            }
            
            SetSize(size);
            //_notInteractable = NFTAsset.Data.nftAction == NFTAction.FollowAnimated;
            _initialized = true;
        }
        [ContextMenu("Resize Collider")]
        public void ResizeColliderAroundChildren() {
            var assetModel = gameObject;
            var pos = assetModel.transform.localPosition;
            var rot = assetModel.transform.localRotation;
            var scale = assetModel.transform.localScale;

            // need to clear out transforms while encapsulating bounds
            assetModel.transform.localPosition = Vector3.zero;
            assetModel.transform.localRotation = Quaternion.identity;
            assetModel.transform.localScale = Vector3.one;

            // start with root object's bounds
            var bounds = new Bounds(Vector3.zero, Vector3.zero);
            if (assetModel.transform.TryGetComponent<Renderer>(out var mainRenderer)) {
                // as mentioned here https://forum.unity.com/threads/what-are-bounds.480975/
                // new Bounds() will include 0,0,0 which you may not want to Encapsulate
                // because the vertices of the mesh may be way off the model's origin
                // so instead start with the first renderer bounds and Encapsulate from there
                bounds = mainRenderer.bounds;
            }


            var descendants = assetModel.GetComponentsInChildren<Transform>();
            foreach (var desc in descendants) {
                if (desc.TryGetComponent<Renderer>(out var childRenderer)) {
                    // use this trick to see if initialized to renderer bounds yet
                    // https://answers.unity.com/questions/724635/how-does-boundsencapsulate-work.html
                    if (bounds.extents == Vector3.zero)
                        bounds = childRenderer.bounds;
                    bounds.Encapsulate(childRenderer.bounds);
                }

                if (desc.TryGetComponent<RectTransform>(out var rectTransform)) {
                    Vector3[] corners = new Vector3[4];
                    rectTransform.GetWorldCorners(corners);

                    foreach (Vector3 corner in corners) {
                        bounds.Encapsulate(corner);
                    }
                }
            }

            var boxCol = _triggerCollider;

            boxCol.center = bounds.center - assetModel.transform.position;
            boxCol.size = bounds.size;
            boxCol.isTrigger = true;
            // restore transforms
            assetModel.transform.localPosition = pos;
            assetModel.transform.localRotation = rot;
            assetModel.transform.localScale = scale;
        }

        public void AddAction(string actionName) {
            var action = _actionsList.GetAction(actionName);
            if (actions.Where(a => a._actionName == action._actionName).FirstOrDefault() != null)
                return;

            var newAction = Instantiate(action);
            actions.Add(newAction);
            newAction.Init(this);
        }

        public void AddActions(List<string> actionsNamesToAdd) {
            var actionsToAdd = _actionsList._nftActions
                .Where(action => actionsNamesToAdd.Contains(action._actionName)).ToList();

            foreach (var action in actionsToAdd) {
                Debug.Log(action._actionName);

                if (actions.FirstOrDefault(a => a._actionName == action._actionName) != null)
                    return;

                var newAction = Instantiate(action);
                actions.Add(newAction);
                newAction.Init(this);
            }
        }

        public void ToggleAction(NftActionAsset action, bool add) {
            if (add) {
                AddAction(action._actionName);
            }
            else {
                RemoveAction(action._actionName);
            }
        }


        public void RemoveAction(string actionName) {
            var assets = actions.Where(a => a._actionName == actionName).ToList();

            foreach (var a in assets) {
                a.Kill();
                actions.Remove(a);
            }
        }

        public void RemoveActions(List<string> actionNamesToRemove) {
            var actionsToRemove = _actionsList._nftActions
                .Where(action => actionNamesToRemove.Contains(action._actionName)).ToArray();

            foreach (var action in actionsToRemove) {
                var assets = actions.Where(a => a._actionName == action._actionName).ToList();

                foreach (var a in assets) {
                    a.Kill();
                    actions.Remove(a);
                }
            }
        }

        public void RemoveActionsOfType(NftActionType type) {
            var assets = actions.Where(a => a._type == type).ToList();
            foreach (var a in assets) {
                a.Kill();
                actions.Remove(a);
            }
        }

        public bool HasActionsOfType(NftActionType type) {
            return actions.Any(action => action._type == type);
        }

        private void UpdateActions() {
            foreach (var action in actions) {
                action.Update();
            }
        }


        public void SetSize(float size) {
            Size = size;
            if (_nft == null)
                return;
            switch (NftTemplate.MediaType) {
                case NFTType.Model:
                    //_nft.transform.localScale = Vector3.Lerp(Vector3.one * NFTAsset.Data.minScale,
                    //    Vector3.one * NFTAsset.Data.maxScale, size);
                    break;
                case NFTType.Video:
                    //_nft.transform.localScale = Vector3.Lerp(Vector3.one * NFTAsset.Data.minScale,
                    //    Vector3.one * NFTAsset.Data.maxScale, size); //VIDEO ERROR
                    break;
                default:
                    //_root.localScale = Vector3.Lerp(Vector3.one * NFTAsset.Data.minScale,
                    //    Vector3.one * NFTAsset.Data.maxScale, size);
                    break;
            }
        }


        private void SetPreviewMaterial() {
            var meshRenderer = _nft.GetComponent<Renderer>();
            if (meshRenderer != null) {
                var mats = meshRenderer.sharedMaterials;
                for (var i = 0; i < mats.Length; i++) {
                    mats[i] = _previewMaterial;
                }

                meshRenderer.sharedMaterials = mats;
            }

            var meshRenderers = _nft.GetComponentsInChildren<Renderer>();
            foreach (var m in meshRenderers) {
                var mats = m.sharedMaterials;
                for (var i = 0; i < mats.Length; i++) {
                    mats[i] = _previewMaterial;
                }

                m.sharedMaterials = mats;
            }
        }

        private void SetColliders() {
            var meshRenderer = _nft.GetComponent<Renderer>();
            if (meshRenderer != null) {
                meshRenderer.gameObject.AddComponent<MeshCollider>();
            }

            var meshRenderers = _nft.GetComponentsInChildren<Renderer>();
            foreach (var m in meshRenderers) {
                m.gameObject.AddComponent<MeshCollider>();
            }
        }

        private async Task GetImageFromIPFS(string hash) {
            
            var www = UnityWebRequestTexture.GetTexture(string.Format("https://ipfs.atomichub.io/ipfs/{0}", hash));
            www.SendWebRequest();
            while(!www.isDone)
                await Task.Yield();
            if(www.error == null) {
                var t = DownloadHandlerTexture.GetContent(www);
                _nft.GetComponent<Image>().sprite = Sprite.Create(t, new Rect(0f, 0f, t.width, t.height), new Vector2(.5f, .5f));
                Debug.Log("Got texture");
            }
        }
        private async void SetNFTType() {
            switch (NftTemplate.MediaType) {
                case NFTType.Image:
                    _nft = Instantiate(_spriteBase, _uiObjectParent);
                    _nft.transform.localRotation = Quaternion.identity;
                    _nft.transform.localScale = Vector3.one;
                    _nft.GetComponent<Image>().sprite = await NFT.GetImageFromIpfs(NftTemplate.IpfsHashes[0]);
                break;
                case NFTType.Model:
                    //_nft = await AddressableLoader.LoadGameObject(NFTAsset.Data.modelRef, _parent);
                    _nft.transform.localRotation = Quaternion.identity;
                    _nft.transform.localScale = Vector3.one;
                    SetColliders();
                    break;
                case NFTType.Book:
                    //_nft = await AddressableLoader.LoadGameObject(NFTAsset.Data.modelRef, _parent);
                    _nft.transform.localRotation = Quaternion.identity;
                    _nft.transform.localScale = Vector3.one;
                    SetColliders();
                    _triggerCollider.enabled = false;
                    break;
                case NFTType.Audio:
                    //_nft = await AddressableLoader.LoadGameObject(NFTAsset.Data.prefabRef, _uiObjectParent);
                    _nft.transform.localRotation = Quaternion.identity;
                    _nft.transform.localScale = Vector3.one;
                    AudioSource s = gameObject.AddComponent<AudioSource>();
                    s.loop = true;
                    //s.clip = NFTAsset.Data.audio;
                    s.maxDistance = 8f;
                    s.minDistance = 3f;
                    s.spatialBlend = 1f;
                    s.rolloffMode = AudioRolloffMode.Linear;
                    s.outputAudioMixerGroup = _audioMixer;
                    s.Play();
                    break;
                case NFTType.Video:
                    _nft = Instantiate(_videoPrefab, _parent);
                    //_nft.GetComponent<VideoPlayer>().clip = NFTAsset.Data.video;
                    _nft.GetComponent<VideoPlayer>().Play();
                    _nft.GetComponent<VideoPlayer>().aspectRatio = VideoAspectRatio.FitVertically;
                    Vector2 scale = new Vector2((float) _nft.GetComponent<VideoPlayer>().clip.width,
                        (float) _nft.GetComponent<VideoPlayer>().clip.height);
                    scale.Normalize();
                    _nft.GetComponent<VideoPlayer>().targetMaterialRenderer.transform.localScale = scale;

                    break;
            }

            SetSize(Size);

            if (NftTemplate.MediaType != NFTType.Book)
                ResizeColliderAroundChildren();
        }

        private void SetNFTAction() {
            if (IsFurniture || _isPreview)
                return;
            //_photonView = GetComponent<PhotonView>();
            /*
            switch (NFTAsset.Data.nftAction) {
                case NFTAction.FollowAnimated:
                case NFTAction.Follow:
                    //if (_photonView.Owner == PhotonNetwork.LocalPlayer) {
                    //    _agent = gameObject.AddComponent<NavMeshAgent>();
                    //    _agent.stoppingDistance = 6f;
                    //    _agent.speed = 12f;
                    //}

                    break;
                case NFTAction.Display:
                    break;
                case NFTAction.Randomize:
                    //if (_photonView.Owner != PhotonNetwork.LocalPlayer)
                    //    return;
                    int i = Random.Range(0, NFTAsset.Data.randomizable.Length - 1);
                    _nft.GetComponent<NFTRandomizer>().Init(NFTAsset.Data.randomizable[i]);
                    SetImage(i);
                    //_photonView.RPC("SetImage", RpcTarget.Others, i);
                    break;
                case NFTAction.Spin:
                    break;
            }
            */
        }

        void Start() {
            if (IsFurniture || _isPreview)
                return;

            if (NftTemplate?.assets[0].mint == 1)
                _mintPS.Play();
            SetNFTAction();
        }


        private void Update() {
            UpdateActions();

            if (!_initialized || IsFurniture || _isPreview)
                return;
            //Missing NFTData!?
            /*
            switch (NFTAsset.Data.nftAction) {
                case NFTAction.FollowAnimated:
                    //if (_photonView.Owner != PhotonNetwork.LocalPlayer)
                    //    return;
                    if (_moveTimer == 0)
                        MoveHere(_player.position);
                    else
                        _moveTimer -= 1;
                    if (_agent.velocity == Vector3.zero && _isWalking)
                        Walk(false);
                    else if (_agent.velocity != Vector3.zero && !_isWalking)
                        Walk(true);
                    break;
                case NFTAction.Follow:
                    if (_moveTimer == 0) {
                        //if (_photonView.Owner != PhotonNetwork.LocalPlayer)
                        //    return;
                        MoveHere(_player.position);
                    }
                    else
                        _moveTimer -= 1;

                    break;
                case NFTAction.Display:
                    break;
            }
            */
        }

        public void MoveHere(Vector3 pos) {
            _moveTimer = 50;
            NavMeshHit hit = new NavMeshHit();
            if (NavMesh.SamplePosition(pos, out hit, 4f, 1))
                _agent.SetDestination(hit.position);
        }

        private void Walk(bool w) {
            _isWalking = w;
            UpdateAnimatorBool("Walk", _isWalking);
            //_photonView.RPC("AnimBool", RpcTarget.Others, "Walk", _isWalking);
        }

        /*
        public void Interact(Tool tool, int itemLevel) {
            if (IsFurniture)
                return;
            //if (_photonView.Owner != PhotonNetwork.LocalPlayer || NFTAsset.Data.nftAction != NFTAction.FollowAnimated)
            //    return;
            UpdateAnimatorTrigger("Interact");
            //_photonView.RPC("AnimTrigger", RpcTarget.Others, "Interact");
        }
        */

        public void Interact() {
            if (IsFurniture)
                return;
            //if (_photonView.Owner != PhotonNetwork.LocalPlayer || NFTAsset.Data.nftAction != NFTAction.FollowAnimated)
            //    return;
            UpdateAnimatorTrigger("Interact");
            //_photonView.RPC("AnimTrigger", RpcTarget.Others, "Interact");
        }

        private Animator _anim;

        private void SetAnimator() {
            if (_nft.GetComponent<Animator>() != null)
                _anim = _nft.GetComponent<Animator>();
            else {
                _anim = _nft.GetComponentInChildren<Animator>();
            }
        }

        public void UpdateAnimatorTrigger(string s) {
            if (_anim == null)
                SetAnimator();
            _anim.SetTrigger(s);
        }

        public void UpdateAnimatorBool(string s, bool b) {
            if (_anim == null)
                SetAnimator();
            _anim.SetBool(s, b);
        }

        //[PunRPC]
        public void AnimTrigger(string s) {
            UpdateAnimatorTrigger(s);
        }

        //[PunRPC]
        public void AnimBool(string s, bool b) {
            UpdateAnimatorBool(s, b);
        }

        //[PunRPC]
        public void SetImage(int i) {
            //_nft.GetComponent<NFTRandomizer>().Init(NFTAsset.Data.randomizable[i]);
        }
    }
}