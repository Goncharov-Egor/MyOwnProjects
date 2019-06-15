#include <iostream>
#include <vector>


using namespace std;

int main(){
    int n;
    cin >> n;
    vector<int> v(n);
    for(int i = 0; i < n; ++i){
        cin >> v[i];
    }
    vector<int> dp(n + 1);
    dp[0] = 0;
    dp[1] = v[0];
    for(int i = 2; i < n + 1; ++i){
        dp[i] = min(dp[i - 1], dp[i - 2]) + v[i - 1];
    }
    cout << dp[n];
}
