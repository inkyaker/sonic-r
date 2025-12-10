import { RegisterObject } from "../ObjectController"
import { Bin } from "@Easy/Core/Shared/Util/Bin"
import Client from "Code/Client/Client"

export default class _OBJBase extends AirshipBehaviour {
    @NonSerialized() public Collider = this.gameObject.GetComponent<BoxCollider>()!
    @NonSerialized() public HomingTarget = false
    @NonSerialized() public HomingWeight = 1
    protected Connections = new Bin()
    protected Debounce = 0
    public readonly meta = {
        AnimationLoader: false
    }

    override Start() {
        if ($CLIENT) {
            this.InitObject()
        }
    }

    public InitObject() {
        RegisterObject(this)
    }

    protected OnTick(GetClient: () => Client) {
        if (this.Debounce > 0) {
            this.Debounce--
        }
    }

    /**
     * Client touched callback
     * @param Client
     */
    protected OnTouch(Client: Client) { }

    /**
     * .RenderStepped callback
     * @param DeltaTime
     */
    protected PreRender(DeltaTime: number) { }

    protected OnRespawn() { }

    public Tick(GetClient: () => Client) {
        this.OnTick(GetClient)
    }

    public TouchClient(Client: Client) {
        if (this.Debounce > 0) { return }

        this.OnTouch(Client)
    }

    public Draw(DeltaTime: number) {
        this.PreRender(DeltaTime)

        if (this.meta.AnimationLoader) {
            (this as unknown as { AnimationController: { Animate: (this: unknown) => void } }).AnimationController.Animate()
        }
    }

    public Respawn() {
        this.OnRespawn()
    }
}