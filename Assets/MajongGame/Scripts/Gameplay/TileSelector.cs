using MajongGame.Common;
using MajongGame.Gameplay.Tiles;
using UnityEngine;

namespace MajongGame.Gameplay
{
    public class TileSelector : MonoBehaviour
    {
        [SerializeField] private SelectedTilesHolder _tilesHolder;

        private void Update()
        {
            if (_tilesHolder == null
                || !GlobalVariablesController.CanClickTiles
                || GlobalVariablesController.LevelPreparing
                || GlobalVariablesController.OnLoadingScene
                || GlobalVariablesController.InPopup
                )
                return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.TryGetComponent(out Tile tile))
                {
                    if (_tilesHolder.CanAddTile() && tile.TryTake())
                    {
                        _tilesHolder.TryAddTile(tile);
                    }
                }
            }
        }
    }
}
