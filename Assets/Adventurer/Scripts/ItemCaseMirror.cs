using UnityEngine;

namespace Adventurer
{
    [RequireComponent(typeof(ItemCaseView))]
    public class ItemCaseMirror : MonoBehaviour
	{
		public ItemData ItemData => generalCase.ItemData;

		private CaseBrain generalCase;
		private ItemCaseView caseView;

		public void Initialize(CaseBrain initializationCase)
		{
			caseView = GetComponent<ItemCaseView>();
			generalCase = initializationCase;
			caseView.Initialize(initializationCase);
        }
	}
}