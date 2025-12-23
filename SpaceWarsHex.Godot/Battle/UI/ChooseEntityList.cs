using Godot;
using SpaceWarsHex.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace SpaceWarsHex
{
    public partial class ChooseEntityList : ItemList
    {
        private readonly List<IHexObject> _items = [];
        private Action<IHexObject>? _onSelect;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Resize();
        }

        public override void _GuiInput(InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseButton)
            {
                if (mouseButton.IsReleased())
                {
                    var index = this.GetItemAtPosition(mouseButton.Position, true);
                    if (index >= 0)
                    {
                        OnItemSelected(index);
                    }
                }
            }
        }

        public void Cancel()
        {
            _items.Clear();
            Clear();
            _onSelect = null;
            Visible = false;
        }

        public void QueueSelect<T>(Vector2 pos, IEnumerable<T> entities, Action<T> onSelect)
            where T : IHexObject
        {
            Position = pos;
            Clear();
            _items.Clear();
            foreach (var entity in entities)
            {
                _items.Add(entity);
                AddItem(entity.Name);
            }

            _onSelect = (e) =>
            {
                if (e is T t)
                {
                    onSelect(t);
                }
                else Cancel();
            };

            Visible = true;
            Resize();
        }

        public void OnItemSelected(int index)
        {
            if (_onSelect is null)
            {
                GD.PushWarning($"{nameof(ChooseEntityList)} received a select event with no action.");
                return;
            }

            var entity = _items[index];
            _onSelect(entity);
            Cancel();
        }

        private void Resize()
        {
            const int padd = 10;
            var font = Theme.DefaultFont;

            float width = 0f;

            for (int i = 0; i < ItemCount; i++)
            {
                var text = GetItemText(i);
                var size = font.GetStringSize(text, fontSize: Theme.DefaultFontSize);
                width = Mathf.Max(width, size.X);
            }

            width += padd * 2;

            Size = new Vector2(width, Size.Y);
        }
    } 
}
