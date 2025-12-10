import { NetworkUtil } from "@Easy/Core/Shared/Util/NetworkUtil"
import { Network } from "Code/Shared/Network"
import DSClient from "./Client"
import { Game } from "@Easy/Core/Shared/Game"
import ClientReplicator from "./Replication"

export default class Framework extends AirshipSingleton {
    override Start() {
        if (!$CLIENT) return

        Network.EnableClient.client.OnServerEvent((NetID, SourcePlayer) => {
            const Object = NetworkUtil.WaitForNetworkIdentityTimeout(NetID, 5)

            if (Object) {
                Object.gameObject.GetAirshipComponent<DSClient>()!.enabled = Game.localPlayer.userId === SourcePlayer
                Object.gameObject.GetAirshipComponent<ClientReplicator>()!.enabled = true
            }
        })
    }
}