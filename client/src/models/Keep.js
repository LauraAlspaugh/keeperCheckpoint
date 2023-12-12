export class Keep {
    constructor(data) {
        this.id = data.id
        this.createdAt = new Date(data.createdAt).toLocaleDateString()
        this.updatedAt = new Date(data.updatedAt).toLocaleDateString()
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.views = data.views
        this.kept = data.kept
        this.creatorId = data.creatorId
        this.creator = data.creator
        this.vaultKeepId = data.vaultKeepId
    }
}
const keepdata = {
    "id": 1,
    "createdAt": "2023-12-08T19:36:39",
    "updatedAt": "2023-12-08T19:36:39",
    "name": "Good Stuff",
    "description": "When you believe",
    "img": "https://images.unsplash.com/photo-1633436374961-09b92742047b?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fExhc2FnbmF8ZW58MHx8MHx8fDA%3D",
    "views": 0,
    "kept": 0,
    "creatorId": "652ef49aff9881f8395eb350",
    "creator": {
        "id": "652ef49aff9881f8395eb350",
        "name": "Laura",
        "email": "lauraalspaugh2002@icloud.com",
        "picture": "https://images.unsplash.com/photo-1617659258448-f2711883be96?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGJsYWNrJTIwYW5kJTIwd2hpdGUlMjBhZXN0aGV0aWN8ZW58MHx8MHx8fDA%3D",
        "vaultId": 0,
        "vaultKeepId": 0
    },
    "vaultId": 0
}