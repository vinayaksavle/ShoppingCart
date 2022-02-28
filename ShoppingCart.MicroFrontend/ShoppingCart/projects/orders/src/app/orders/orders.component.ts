import { Component } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { DialogBoxComponent } from "../dialog-box/dialog-box.component";
import { Orders } from "../models/orders";
import { OrdersService } from "../services/orders.service";

@Component({
    selector: 'app-orders',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.css']
})
export class OrdersComponent {
    orders?: Orders[];
    inputParams: Orders;
    //orders?: Orders[] = [{ id: "123", name: "vvs", price: 12, manufacturer: "tvs" }];
    displayedColumns: string[] = ['id', 'name', 'price', 'manufacturer', 'action'];

    constructor(public dialog: MatDialog, private ordersService: OrdersService) { }

    ngOnInit() {
        this.getAllOrders();
    }

    getAllOrders() {
        this.ordersService.getAll().subscribe({
            next: (data) => {
                this.orders = data;
                console.log(this.orders);
            }
        })
    }

    openDialog(action: string, payload: any) {
        payload.action = action;

        const dialogRef = this.dialog.open(DialogBoxComponent, {
            width: '300px',
            data: payload
        })

        dialogRef.afterClosed().subscribe(result => {
            if (!(result === null || result === undefined)) {
                if (result.event == 'Add')
                    this.addOrder(result.data);
                else if (result.event == 'Update')
                    this.updateOrder(result.data);
                else if (result.event == 'Delete')
                    this.deleteOrder(result.data);
            }

        })
    }

    addOrder(payload: any) {
        this.inputParams = {
            name: payload.name,
            price: payload.price,
            manufacturer: payload.manufacturer
        };

        this.ordersService.create(this.inputParams).subscribe({
            next: (data) => {
                console.log(data);
                this.getAllOrders();
            }
        })
    }

    updateOrder(payload: any) {

    }

    deleteOrder(payload: any) {
        this.ordersService.delete(payload.id).subscribe({
            next: (data) => {
                console.log(data);
                this.getAllOrders();
            }
        })
    }
}