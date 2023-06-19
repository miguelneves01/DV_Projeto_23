using UnityEngine;

namespace Assets.Buildings.Scripts
{
    public class MinePlacedBuilding : PlacedBuilding
    {
        private int _coinsStored = 0;
        private readonly int _coinsLimit = 1000;
        private float _curTime = 0;
        private float _coinsRate = 2;

        public override void Interact()
        {
            CurrencySystem.Instance.AddCurrency(_coinsStored);
            _coinsStored = 0;
        }

        private void Update()
        {
            if (_coinsStored >= _coinsLimit)
            {
                return;
            }

            _curTime += Time.deltaTime;

            if (_curTime >= 1/_coinsRate)
            {
                _coinsStored++;
                _curTime = 0;
            }
        }
    }
}