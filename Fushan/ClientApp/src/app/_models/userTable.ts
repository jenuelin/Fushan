import { TableBase } from '@app/_models';

export class UserTable implements TableBase {
  userID: string;
  userName: string;
  sex: string;
  sexString: string;
  rank: string;
  level: string;
}
