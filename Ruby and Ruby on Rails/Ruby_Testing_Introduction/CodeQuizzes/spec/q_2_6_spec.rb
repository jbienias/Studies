require 'q_2_6'

describe "#super_compact" do
  it "removes nil and empty elements" do
    arr = [:bob, "", nil, [], "joe"]
    expected = [:bob, "joe"]
    expect(arr.super_compact).to eq expected
  end
end
