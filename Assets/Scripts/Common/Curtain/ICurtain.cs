using Cysharp.Threading.Tasks;

namespace Common.Curtain
{
    public interface ICurtain
    {
        public UniTask Show();
        public UniTask Hide();
    }
}