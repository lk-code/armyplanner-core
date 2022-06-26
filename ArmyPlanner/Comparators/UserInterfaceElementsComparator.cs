using ArmyPlanner.Models.Codices;

namespace ArmyPlanner.Comparators
{
    public class UserInterfaceElementsComparator
    {
        #region properties

        private readonly string _elementKey;

        #endregion

        #region constrcutors

        public UserInterfaceElementsComparator(string elementKey)
        {
            this._elementKey = elementKey.ToLowerInvariant() ?? throw new System.ArgumentNullException(nameof(elementKey));
        }

        #endregion

        #region logic

        public bool CodexUnitList() => (this._elementKey == UserInterfaceElements.CODEX_UNIT_LIST);
        public bool CodexWargearList() => (this._elementKey == UserInterfaceElements.CODEX_WARGEAR_LIST);
        public bool CodexWeaponList() => (this._elementKey == UserInterfaceElements.CODEX_WEAPON_LIST);
        public bool RosterDetailSummaryHeader() => (this._elementKey == UserInterfaceElements.ROSTER_DETAIL_SUMMARY_HEADER);

        #endregion
    }
}
