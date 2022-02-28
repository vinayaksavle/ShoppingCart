import { Component, Inject, Optional } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Orders } from "../models/orders";

@Component({
    selector: 'app-dialog-box',
    templateUrl: './dialog-box.component.html',
    styleUrls: ['./dialog-box.component.css']
})
export class DialogBoxComponent {
    action: string;
    passedData: any;

    constructor(public dialogRef: MatDialogRef<DialogBoxComponent>, @Optional() @Inject(MAT_DIALOG_DATA) public data: Orders) {
        this.passedData = { ...data };
        this.action = this.passedData.action;
    }

    onAction() {
        this.dialogRef.close({ event: this.action, data: this.passedData })
    }

    closeDialog() {
        this.dialogRef.close({ event: 'Cancel' });
    }
}