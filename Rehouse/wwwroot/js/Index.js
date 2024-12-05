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
            rows: {},
            page: 1,
            totalPages: 0,
            currentPage: 1,
            searchName:null
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
            // 轉換 type 從文字到數字
            const convertedItem = {
                index:index,
                ...item,
                type: item.type === '公寓' ? 1 : 2
            };

            // 複製轉換後的資料到表單
            this.property = { ...convertedItem };
            this.editModal.show();
        },
        closeAddModal() {
            this.addModal.hide();
        },
        closeEditModal() {
            this.editModal.hide();
        },
        validateForm() {
            // 檢查所有屬性是否為空或null
            return Object.values(this.property).every(value => {
                if (typeof value === 'string') {
                    return value.trim() !== '';
                }
                return value !== null;
            });
        },
        async changeDataStatus(index) {
            const data = this.rows[index];
            const id = data.id;
            try {
                const response = await axios.patch(`api/estates/${id}/status`);
                await this.GetUserEstate();
                alert('狀態修改成功');
            }
            catch (error) {
                console.error(error);
            }
        },
        async deleteProcess(index) {
            const result = await Swal.fire({
                title: "確定要刪除嗎?",
                text: "此操作無法復原!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "是",
                cancelButtonText: "否"
            });

            if (result.isConfirmed) {
                await this.deleteData(index);
                await Swal.fire({
                    title: "已刪除!",
                    text: "資料已成功刪除",
                    icon: "success"
                });
                await this.GetUserEstate();
            }
        },
        async deleteData(index) {
            const data = this.rows[index];
            try {
                await axios.delete(`api/estates/${data.id}/delete`);
                this.rows.splice(index, 1);
            } catch (error) {
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

                const response = await axios.put(`api/estates/${id}`, updateData);
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
                this.resetForm();
                await this.GetUserEstate();
                this.closeAddModal();
            }
            catch (error) {
                console.error(error);
                alert('新增失敗');
            }
        },
        async GetUserEstate() {
            // Trim前先檢查是否為空值
            const searchName = this.searchName?.trim() || null;
            try {
                const response = await axios.get(`api/estates/my?page=${this.page}&searchName=${searchName}`);
                this.rows = response.data.data.estateList;
                this.totalPages = Math.ceil(response.data.data.totalCount / 5);
                this.currentPage = this.page;
            } catch (error) {
                console.error(error);
                alert('抓取失敗');
            }
        },
        changePage(page) {
            if (page >= 1 && page <= this.totalPages) {
                this.page = page;
                this.GetUserEstate();
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
    },
    watch: {
        searchName: function (newVal) {
            if (!newVal) {
                this.GetUserEstate();
            }
        }
    }
}).mount("#app");