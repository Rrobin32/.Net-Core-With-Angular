import { Component, Input, OnInit } from '@angular/core';
import { UserService } from '../../../services/user/user.service';

@Component({
  selector: 'app-add-edit-user',
  templateUrl: './add-edit-user.component.html',
  styleUrls: ['./add-edit-user.component.css']
})
export class AddEditUserComponent implements OnInit {

  constructor(private service: UserService) { }
  @Input() usr: any;
  Id = 0;
  UserName = "";
  FirstName = "";
  LastName = "";
  Password = "";
  CreatedOn = new Date().toISOString();
  CreatedBy = 1;
  ModifiedOn = new Date().toISOString()
  ModifiedBy = 1

  pageSize: number = 10;
  pageIndex: number = 0;

  ngOnInit(): void {
    this.loadUserList();
  }

  loadUserList() {
    var queryString = "?pageIndex=" + this.pageIndex + "&pageSize=" + this.pageSize;
    this.service.getUserList(queryString).subscribe((res: any) => {
      this.Id = this.usr.Id;
      this.UserName = this.usr.UserName;
      this.FirstName = this.usr.FirstName;
      this.LastName = this.usr.LastName;
      this.Password = this.usr.Password
    });
  }

  addUser() {
    var val = {
      UserName: this.UserName,
      Password: this.Password,
      FirstName: this.FirstName,
      LastName: this.LastName,
      IsActive: true,
      CreatedBy: this.CreatedBy,
      CreatedOn: this.CreatedOn,
      ModifiedBy: this.ModifiedBy,
      ModifiedOn: this.ModifiedOn
    };

    this.service.addUser(val).subscribe(res => {
      if (res.ResponseMessage[0].CustomCode > 0) {
        this.errorResposeMessage(res);
      }
      else {
        alert(res.ResponseData.AddUserInfoResponseObj.Message);
      }
    },
      (error: any) => {
        if (error.status == 401 || error.status == 0) {
          alert('Un-authorized!!');
        }
      });
  }

  updateUser() {
    var val = {
      Id: this.Id,
      UserName: this.UserName,
      Password: this.Password,
      FirstName: this.FirstName,
      LastName: this.LastName,
      IsActive: true,
      ModifiedBy: this.ModifiedBy,
      ModifiedOn: this.ModifiedOn,
      CreatedBy: this.usr.CreatedBy,
      CreatedOn: this.usr.CreatedOn
    };

    this.service.updateUser(val).subscribe(res => {
      if (res.ResponseMessage[0].CustomCode > 0) {
        this.errorResposeMessage(res);
      }
      else {
        alert(res.ResponseData.UpdateUserInfoResponseObj.Message);
      }
    },
      (error: any) => {
        if (error.status == 401 || error.status == 0) {
          alert('Un-authorized!!');
        }
      });
  }

  errorResposeMessage(res: any) {
    var errorMsg = "";
    for (let i = 0; i < res.ResponseMessage.length; i++) {
      errorMsg += res.ResponseMessage[i].Message + "\n"
    }
    alert(errorMsg.trimEnd());
  }
}
