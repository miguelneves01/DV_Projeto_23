using UnityEngine;

namespace Assets.Buildings.Scripts
{
    public class MinePlacedBuilding : PlacedBuilding
    {
        private readonly int _coinsLimit = 1000;
        private readonly float _coinsRate = 2;
        private int _coinsStored;
        private float _curTime;

        public override void Interact()
        {
            CurrencySystem.Instance.AddCurrency(_coinsStored);
            _coinsStored = 0;
        }

        private void Update()
        {
            if (_coinsStored >= _coinsLimit) return;

            _curTime += Time.deltaTime;

            if (_curTime >= 1 / _coinsRate)
            {
                _coinsStored++;
                _curTime = 0;
            }
        }
    }
}