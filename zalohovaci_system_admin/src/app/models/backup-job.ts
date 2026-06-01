
export class BackupJob {
  public id: number;
  public jobId: string;
  public sources: string[];
  public targets: string[];
  public retentionSize: number;
  public retentionCount: number;
  public method: string;
  public timing: string;
  public createdAt: string;
}
