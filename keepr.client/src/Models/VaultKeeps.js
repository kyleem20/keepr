class VaultKeeps {
    constructor(data) {
        this.id = data.id
        this.creatorId = data.creatorId
        this.vaultId = data.vaultId
        this.keepId = data.keepId
    }
}

export class Keeps extends VaultKeeps {
    constructor(data) {
        super(data)
        this.id = data.keep.id
        this.name = data.keep.name
        this.description = data.keep.description
        this.img = data.keep.img
        this.views = data.keep.views
        this.keeps = data.keep.keeps
        this.shares = data.keep.shares
        this.creatorId = data.keep.creatorId
        this.creator = data.keep.creator || {}
    }
}
export class Vaults extends VaultKeeps {
    constructor(data) {
        super(data)
        this.id = data.vault.id
        this.name = data.vault.name
        this.description = data.vault.description
        this.img = data.vault.img
        this.isPrivate = data.vault.isPrivate
        this.creatorId = data.vault.creatorId
        this.creator = data.vault.creator || {}
    }
}