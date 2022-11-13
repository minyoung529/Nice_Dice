/// <summary>
/// UIManager 등 컨트롤러들이 상속받는 클래스.
/// 코드 재사용 용도.
/// </summary>
public class ControllerBase
{
    public virtual void OnAwake() { }
    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
}
