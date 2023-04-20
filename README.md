# Klem's Toolbelt
> A collection of tools for my personal use

## Tools

### Bounds2DComponent
A component that can be added to any entity to define a 2D bounding box.
It can be generated from a sprite renderer, a Collider2D, or manually.

### ObservableVariable<T>
A variable of any type that can be observed for changes via UnityAction callbacks.
```csharp
 public class InventoryUiSlot : MonoBehaviour
 {
     [SerializeField] private ObservableVariable<int> stackSize;
     [SerializeField] private Text _itemCountText;
 
     private void Awake()
     {
         stackSize.OnValueChanged += OnStackSizeChanged;
     }
 
     private void OnStackSizeChanged(int oldCount, int newCount)
     {
         _itemCountText.text = newCount.ToString();
     }
 }
```

