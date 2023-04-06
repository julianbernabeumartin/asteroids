// Interfaces for the update manager. So we can use it for Update, FixedUpdate or LateUpdate

public interface IUpdate
{
    void IUpdate();
}

public interface IFixedUpdate
{
    void IFixedUpdate();
}

public interface ILateUpdate
{
    void ILateUpdate();
}
