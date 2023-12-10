export class Vault {
    constructor(data) {
        this.id = data.id
        this.createdAt = new Date(data.createdAt).toLocaleDateString()
        this.updatedAt = new Date(data.updatedAt).toLocaleDateString()
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.isPrivate = data.isPrivate
        this.creatorId = data.creatorId
        this.creator = data.creator

    }
}
export const vaultData = {
    "id": 1,
    "createdAt": "2023-12-09T22:38:16",
    "updatedAt": "2023-12-09T22:38:16",
    "name": "Cool Stuff",
    "description": "A Collection of all my favorite things",
    "img": "https://images.unsplash.com/photo-1552508744-1696d4464960?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=500&h=400&q=60",
    "isPrivate": false,
    "creatorId": "652ef49aff9881f8395eb350",
    "creator": {
        "id": "652ef49aff9881f8395eb350",
        "name": "Laura",
        "email": "lauraalspaugh2002@icloud.com",
        "picture": "https://images.unsplash.com/photo-1617659258448-f2711883be96?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGJsYWNrJTIwYW5kJTIwd2hpdGUlMjBhZXN0aGV0aWN8ZW58MHx8MHx8fDA%3D",
        "bio": "Hey there. Trust the process. No one finds success overnight....trust it ",
        "coverImg": null,
        "vaultId": 0,
        "vaultKeepId": 0
    }
}