using MajongGame.Common;
using UnityEngine;

namespace MajongGame.Gameplay
{
    public class TileSelector : MonoBehaviour
    {
        [SerializeField] private SelectedTilesHolder _tilesHolder;

        private void Update()
        {
            if (_tilesHolder == null || !GlobalVariablesController.CanClickOnTiles) 
                return;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.TryGetComponent(out Tile tile))
                {
                    if (!tile.IsTaked && _tilesHolder.TryAddTile(tile))
                    {
                        tile.SetTaked();
                    }
                }
            }
        }
    }
}
