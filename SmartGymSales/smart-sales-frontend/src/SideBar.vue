<template lang="html">

  <div ref="parentSidebar" class="parentx-static">

    <vs-sidebar  :parent="$refs.parentSidebar" static-position vsBackgroundHidden  default-index="1" color="primary" calss="custom-sidebar" spacer v-model="active">

      <div class="header-sidebar" slot="header">
        <vs-avatar  size="70px" src="https://randomuser.me/api/portraits/men/85.jpg"/>

        <h4>
          My Name
          <vs-button color="primary" icon="more_horiz" type="flat"></vs-button>
        </h4>

      </div>

      <vs-sidebar-item index="1" icon="question_answer">
        Dashboard
      </vs-sidebar-item>
      <vs-sidebar-item index="2" icon="gavel">
        History
      </vs-sidebar-item>

      <vs-divider icon="person" position="left">
        User
      </vs-divider>

      <vs-sidebar-item index="3" icon="verified_user">
        Configurations
      </vs-sidebar-item>
      <vs-sidebar-item index="4" icon="account_box">
        Profile
      </vs-sidebar-item>
      
      <div class="footer-sidebar" slot="footer">
        <vs-button icon="reply" color="danger" type="flat">log out</vs-button>
        <vs-button icon="settings" color="primary" type="border"></vs-button>
      </div>

    </vs-sidebar>
  </div>

</template>

<script>
export default {
  data: () => ({
    active: false
  })
}
</script>

<style lang="stylus">
.parentx-static {
  overflow: hidden;
  height: 100vh;
  left:0;
  top:0;
  box-sizing: content-box;
  width: 19%;
  max-width: 100%;
  padding: 0;
}
.vs-sidebar.vs-sidebar-staticPosition{
  position :absolute;
}
.custom-sidebar{
 position: absolute;
  display:flex;
  flex-flow :column nowrap;
  top:0;
  width :100%;
}
.header-sidebar {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  width: 100%;

  h4 {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 100%;

    > button {
      margin-left: 10px;
    }
  }
}

.footer-sidebar {
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: 100%;

  > button {
    border: 0px solid rgba(0, 0, 0, 0) !important;
    border-left: 1px solid rgba(0, 0, 0, 0.07) !important;
    border-radius: 0px !important;
  }
}
</style>