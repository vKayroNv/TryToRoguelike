namespace TryToRoguelike.Menus
{
    public abstract class Menu : Updatable
    {
        private bool _isActive;

        public void Activate()
        {
            _isActive = true;

            while (_isActive)
            {
                Draw();
                Update();

                System.Threading.Thread.Sleep(50);
            }
        }

        protected void Deactivate()
        {
            _isActive = false;
        }
    }
}
