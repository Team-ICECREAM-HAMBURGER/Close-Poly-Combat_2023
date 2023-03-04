using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponUIController : MonoBehaviour {
    public static WeaponUIController instance;

    [HideInInspector] public AmmoEvent onAmmoEvent = new AmmoEvent();
    [HideInInspector] public MagazineEvent onMagzineEvent = new MagazineEvent();


    [SerializeField] private TextMeshProUGUI textAmmo;
    [SerializeField] private Transform magazineUIParent;
    [SerializeField] private GameObject magazineUIClone;

    private List<GameObject> magazineList;
        public List<GameObject> MagazineList {
            get {
                return this.magazineList;
            }
            set {
                this.magazineList = value;
            }
        }


    private void Init() {
        if (instance == null) {
            instance = this;
        }

        this.onAmmoEvent.AddListener(UpdateAmmoHUD);
        this.onMagzineEvent.AddListener(UpdateMagazineHUD);
        this.magazineList = new List<GameObject>();
    }

    private void Awake() {
        Init();
    }

    private void UpdateAmmoHUD(int currentAmmo, int maxAmmo) {
        this.textAmmo.text = $"{currentAmmo}/{maxAmmo}";
    }

    private void UpdateMagazineHUD(int currentMagazine) {
        for (int i = 0; i < this.magazineList.Count; ++i) { // 다 끄기
            this.magazineList[i].SetActive(false);
        }

        for (int i = 0; i < currentMagazine; ++i) { // 현재 탄창 수만큼 켜기 (Update)
            this.magazineList[i].SetActive(true);
        }
    }

    public void SetupMagazine(int currentMagazine, int maxMagazine) {
        for (int i = 0; i < maxMagazine; ++i) {  // 탄창 UI 아이콘 프리팹 생성
            GameObject clone = Instantiate(this.magazineUIClone);

            clone.transform.SetParent(this.magazineUIParent);
            clone.gameObject.SetActive(false);
            
            this.magazineList.Add(clone);
        }

        for (int i = 0; i < currentMagazine; ++i) {  // 아이콘 활성화 (초기화)
            this.magazineList[i].SetActive(true);
        }
    }





}