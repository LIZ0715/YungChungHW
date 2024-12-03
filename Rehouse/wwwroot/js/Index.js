const app = Vue.createApp({
    data() {
        return {
            name: '123',
            addModal: null,
            editModal:null,
            property: {
                name: '',
                address: '',
                price: 0,
                type: 1,
                range: 0,
                status:true
            },
            rows: {}
        }
    },
    mounted() {
        // 初始化 Bootstrap Modal
        this.addModal = new bootstrap.Modal(document.getElementById('addModal'));
        this.editModal = new bootstrap.Modal(document.getElementById('editModal'));    },
    methods: {
        openAddModal() {
            this.resetForm();
            this.addModal.show();
        },
        openEditModal(item,index) {
            console.log(item);
            // 轉換 type 從文字到數字
            const convertedItem = {
                index:index,
                ...item,
                type: item.type === '公寓' ? 1 : 2
            };

            // 複製轉換後的資料到表單
            this.property = { ...convertedItem };
            console.log(this.property);
            this.editModal.show();
        },
        closeAddModal() {
            this.addModal.hide();
        },
        closeEditModal() {
            this.editModal.hide();
        },
        validateForm() {
            console.log(this.property)
            // 檢查所有屬性是否為空或null
            return Object.values(this.property).every(value => {
                if (typeof value === 'string') {
                    return value.trim() !== '';
                }
                //if (typeof value === 'number' && value!=='index') {
                //    return value !== null && value > 0;
                //}
                return value !== null;
            });
        },
        async ChangeDataStatus(index) {
            const data = this.rows[index];
            const id = data.id;
            try {
                const response = await axios.delete(`api/estates/${id}`);
                await this.GetUserEstate();
                alert('狀態修改成功');
            }
            catch (error) {
                console.error(error);
            }
        },
        async EditForm(index) {
            const data = this.rows[index];
            const id = data.id;

            try {
                // 創建要更新的資料物件
                const updateData = {
                    id: id,
                    name: this.property.name,
                    address: this.property.address,
                    price: Number(this.property.price),
                    type: Number(this.property.type),
                    range: Number(this.property.range)
                };

                console.log('Updating data:', updateData);

                const response = await axios.put(`api/estates/${id}`, updateData);
                console.log('Update response:', response);

                // 更新本地資料
                this.rows[index] = { ...updateData };

                alert('更新成功');
                this.editModal.hide();
            }
            catch (error) {
                console.error('Update failed:', error);
                alert('更新失敗：' + (error.response?.data?.message || '未知錯誤'));
            }
        },
        async submitForm() {
            if (!this.validateForm()) {
                alert('請填寫所有欄位');
                return;
            }

            console.log('表單資料:', this.property);

            //使用axios 把資料送去後端
            try {
                const response = await axios.post('api/estates', this.property);
                console.log(response);
                alert('新增成功');

                const convertedItem = {
                    ...response.data.data,
                    type: response.data.data.type === 1 ? "公寓" : "透天"
                };
                this.rows.push(convertedItem);
                console.log(this.rows);
                this.resetForm();
                this.closeAddModal();
            }
            catch (error) {
                console.error(error);
                alert('新增失敗');
            }
        },
        async GetUserEstate() {
            try {
                const response = await axios.get('api/estates/my');
                console.log(response);
                this.rows = response.data.data;
                console.log(this.rows)
            }
            catch (error) {
                console.error(error);
                alert('抓取失敗')
            }
        },
        resetForm() {
            this.property = {
                name: '',
                address: '',
                price: 0,
                type: 1,
                size: 0
            };
        }
    },
    async created() {
        await this.GetUserEstate();
    }
}).mount("#app");