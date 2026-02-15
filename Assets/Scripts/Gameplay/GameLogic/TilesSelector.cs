using Gameplay.Tiles;
using UniRx;
using UnityEngine;

namespace Gameplay
{
    public class TilesSelector
    {
        private readonly SelectedTilesHolder _tilesHolder;
        private CompositeDisposable _disposables = new CompositeDisposable();
        public bool CanClick;

        public TilesSelector(SelectedTilesHolder tilesHolder)
        {
            _tilesHolder = tilesHolder;

            Observable.EveryUpdate()
                .Where(_ => CanClick && Input.GetMouseButtonDown(0))
                .Subscribe(_ => OnMouseClick())
                .AddTo(_disposables);
        }

        private void OnMouseClick()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.TryGetComponent(out Tile tile))
            {
                if (_tilesHolder.CanAddTile() && !tile.Taked.Value)
                {
                    tile.OnTaked();
                    _tilesHolder.AddTile(tile);
                }
                else tile.OnCantTake();
            }
        }
    }
}
