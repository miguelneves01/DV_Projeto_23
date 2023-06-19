namespace Assets.Buildings.Scripts
{
    public class DefaultPlacedBuilding : PlacedBuilding
    {
        public override void Interact()
        {
            ShopManager.Instance.ShowShop(PlacedBuildingSO.Name);
        }
    }
}