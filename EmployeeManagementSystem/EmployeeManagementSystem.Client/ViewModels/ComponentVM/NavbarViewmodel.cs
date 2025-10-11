using Shared.Enums;
namespace EmployeeManagementSystem.Client.ViewModels.ComponentVM
{
    public class NavbarViewmodel
    {
        private NavMenuItem _selected;
        public NavMenuItem Selected
        {
            get => _selected;
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    NotifyStateChanged();
                }
            }
        }

        public void OnSelect(NavMenuItem item)
        {
            Selected = item;
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
