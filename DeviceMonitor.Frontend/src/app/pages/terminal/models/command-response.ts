import { Status } from "../enums/status.enum";

export class CommandResponse {
  status: Status = Status.Unknown;
  output: string = "";
  statusMessage: string = "";
}

