import { Status } from "../enums/status.enum"

export class ConnectResponse {
  status: Status = Status.Unknown;
  statusMessage: string = ""
}
