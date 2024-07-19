using Adventurer;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace EvolveGames
{
    public class ItemChange : IItemHandler, ITickable, IFixedTickable
    {
        [Header("Item Change")]
        [SerializeField] public Animator ani;
        [SerializeField] bool LoopItems = true;
        [SerializeField, Tooltip("You can add your new item here.")] GameObject[] Items;
        [SerializeField, Tooltip("These logos must have the same order as the items.")] Sprite[] ItemLogos;
        [SerializeField] int ItemIdInt;
        int MaxItems;
        int ChangeItemInt;
        [HideInInspector] public bool DefiniteHide;
        bool ItemChangeLogo;
        private Image _itemCanvasLogo;
        private ICoroutineStarter coroutineStarter;

        [Inject]
        private void Construct(ICoroutineStarter coroutineStarter, MENU menu, IHandAnimatable animator)
        {
            this.coroutineStarter = coroutineStarter;
            _itemCanvasLogo = menu.Logo;
            ani = animator.GetHandsAnimator();
            Color OpacityColor = _itemCanvasLogo.color;
            OpacityColor.a = 0;
            _itemCanvasLogo.color = OpacityColor;
            ItemChangeLogo = false;
            DefiniteHide = false;
            ChangeItemInt = ItemIdInt;

            if (ItemLogos != null && ItemLogos.Length > 0)
            {
                _itemCanvasLogo.sprite = ItemLogos[ItemIdInt];
                MaxItems = Items.Length - 1;
                this.coroutineStarter.StartCoroutine(ItemChangeObject());
            }
        }

        public void Hide(bool Hide)
        {
            DefiniteHide = Hide;
            ani.SetBool("Hide", Hide);
        }

        IEnumerator ItemChangeObject()
        {
            if(!DefiniteHide) ani.SetBool("Hide", true);
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < (MaxItems + 1); i++)
            {
                Items[i].SetActive(false);
            }
            Items[ItemIdInt].SetActive(true);
            if (!ItemChangeLogo) this.coroutineStarter.StartCoroutine(ItemLogoChange());

            if (!DefiniteHide) ani.SetBool("Hide", false);
        }

        IEnumerator ItemLogoChange()
        {
            ItemChangeLogo = true;
            yield return new WaitForSeconds(0.5f);
            _itemCanvasLogo.sprite = ItemLogos[ItemIdInt];
            yield return new WaitForSeconds(0.1f);
            ItemChangeLogo = false;
        }

        public void Tick()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                ItemIdInt++;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                ItemIdInt--;
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                if (ani.GetBool("Hide")) Hide(false);
                else Hide(true);
            }

            if (ItemIdInt < 0) ItemIdInt = LoopItems ? MaxItems : 0;
            if (ItemIdInt > MaxItems) ItemIdInt = LoopItems ? 0 : MaxItems;


            if (ItemIdInt != ChangeItemInt)
            {
                ChangeItemInt = ItemIdInt;
                this.coroutineStarter.StartCoroutine(ItemChangeObject());
            }
        }

        public void FixedTick()
        {
            if (ItemChangeLogo)
            {
                Color OpacityColor = _itemCanvasLogo.color;
                OpacityColor.a = Mathf.Lerp(OpacityColor.a, 0, 20 * Time.deltaTime);
                _itemCanvasLogo.color = OpacityColor;
            }
            else
            {
                Color OpacityColor = _itemCanvasLogo.color;
                OpacityColor.a = Mathf.Lerp(OpacityColor.a, 1, 6 * Time.deltaTime);
                _itemCanvasLogo.color = OpacityColor;
            }
        }

        public ItemType GetCurrentItemType()
        {
            return ItemType.Gun;
        }
    }
}
