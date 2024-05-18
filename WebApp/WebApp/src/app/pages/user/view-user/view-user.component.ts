import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user/user.service';

@Component({
  selector: 'app-view-user',
  templateUrl: './view-user.component.html',
  styleUrls: ['./view-user.component.css']
})
export class ViewUserComponent implements OnInit {

  constructor(private service: UserService) { }

  UserList: any = [];
  ModalTitle = "";
  ActivateAddEditUserComp: boolean = false;
  usr: any;

  pageSize: number = 10;
  pageIndex: number = 0;

  ngOnInit(): void {
    this.refreshUserList();
  }

  addClick() {
    this.usr = {
      Id: "0",
      UserName: "",
      FirstName: "",
      LastName: "",
      Password: "",
      CreatedOn: new Date().toISOString(),
      CreatedBy: 1,
      ModifiedOn: new Date().toISOString(),
      ModifiedBy: 1
    }
    this.ModalTitle = "Add User";
    this.ActivateAddEditUserComp = true;
  }

  editClick(item: any) {
    this.usr = item;
    this.ModalTitle = "Edit User";
    this.ActivateAddEditUserComp = true;
  }

  deleteClick(item: any) {
    if (confirm('Are you sure??')) {

      var val = {
        Id: item.Id
      };

      this.service.deleteUser(val).subscribe(res => {
        console.log(res);
        if (res.ResponseMessage[0].CustomCode > 0) {
          this.errorResposeMessage(res);
        }
        else {
          alert(res.ResponseData.DeleteUserInfoResponseObj.Message.toString());
          this.refreshUserList();
        }
      },
        (error: any) => {
          if (error.status == 401 || error.status == 0) {
            alert('Un-authorized!!');
          }
        }
      );
    }
  }

  closeClick() {
    this.ActivateAddEditUserComp = false;
    this.refreshUserList();
  }

  refreshUserList() {
    var queryString = "?pageIndex=" + this.pageIndex + "&pageSize=" + this.pageSize;
    this.service.getUserList(queryString).subscribe((res: any) => {
      this.UserList = res.ResponseData.UserResponse;
    },
      (error: any) => {
        if (error.status == 401 || error.status == 0) {
          alert('Un-authorized!!');
        }
      }
    );
  }

  errorResposeMessage(res: any) {
    var errorMsg = "";
    for (let i = 0; i < res.ResponseMessage.length; i++) {
      errorMsg += res.ResponseMessage[i].Message + "\n"
    }
    alert(errorMsg.trimEnd());
  }
}
