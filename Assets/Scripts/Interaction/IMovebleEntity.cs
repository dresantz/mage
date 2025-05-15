using UnityEngine;

// Interface para acessar o speed e o transform de objetos sem se importar se � player ou enemy
public interface IMovableEntity
{
    float GetSpeed();
    void SetSpeed(float newSpeed);
    Transform GetTransform(); // �til para acessar transform
}
